#region ORM'siz Yaklaşım (SQL + Kod)
//using Microsoft.Data.SqlClient;
//using Microsoft.Extensions.Configuration;

//ConfigurationManager configuration = new();
//configuration.AddJsonFile("appsettings.json");

//SqlConnection connection = new($"Server=localhost, 1433;Database=Northwind;User Id=sa;Password={configuration["Password"]}");
//await connection.OpenAsync();

//SqlCommand command = new(@"
//SELECT employee.FirstName, product.ProductName, COUNT(*) [Count] FROM Employees employee
//INNER JOIN Orders orders
//	ON employee.EmployeeID = orders.EmployeeID
//INNER JOIN [Order Details] orderDetail
//	ON orders.OrderID = orderDetail.OrderID
//INNER JOIN Products product
//	ON orderDetail.ProductID = product.ProductID
//GROUP By employee.FirstName, product.ProductName
//ORDER By COUNT(*) DESC
//", connection);

//SqlDataReader dr = command.ExecuteReader();
//while (await dr.ReadAsync())
//{
//    Console.WriteLine($"{dr["FirstName"]} {dr["ProductName"]} {dr["Count"]}");
//}

//await connection.CloseAsync();
#endregion

#region ORM'li Yaklaşım (SQL - Kod)
using Microsoft.EntityFrameworkCore;
using ORM_SQL_İzalasyonu.Models;

NorthwindContext context = new();
#region Kod 1
//var query = context.Employees
//    .Include(employee => employee.Orders)
//        .ThenInclude(order => order.OrderDetails)
//        .ThenInclude(orderDetail => orderDetail.Product)
//    .SelectMany(employee => employee.Orders, (employee, order) => new { employee.FirstName, order.OrderDetails })
//    .SelectMany(data => data.OrderDetails, (data, orderDetail) => new { data.FirstName, orderDetail.Product.ProductName })
//    .GroupBy(data => new
//    {
//        data.ProductName,
//        data.FirstName
//    })
//    .Select(data => new
//    {
//        data.Key.FirstName,
//        data.Key.ProductName,
//        Count = data.Count()
//    });

//var data = await query.ToListAsync();
#endregion
#region Kod 2
var query = from employee in context.Employees
            join order in context.Orders
                on employee.EmployeeId equals order.EmployeeId
            join orderDetail in context.OrderDetails
                on order.OrderId equals orderDetail.OrderId
            join product in context.Products
                on orderDetail.ProductId equals product.ProductId
            select new { employee.FirstName, product.ProductName } into data
            group data by new { data.FirstName, data.ProductName } into result
            select new
            {
                result.Key.FirstName,
                result.Key.ProductName,
                Count = result.Count()
            };

var datas = await query.ToListAsync();
#endregion

Console.WriteLine();
#endregion