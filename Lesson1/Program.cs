using Lesson1;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;


#region Kod İçinde SQL
ConfigurationManager configuration = new();
configuration.AddJsonFile("appsettings.json");

await using SqlConnection connection = new($"Server=localhost, 1433;Database=Northwind;User Id=sa;Password={configuration["Password"]}");
await connection.OpenAsync();

SqlCommand command = new("Select * from Employees", connection);
SqlDataReader dr = await command.ExecuteReaderAsync();
while (await dr.ReadAsync())
{
    Console.WriteLine($"{dr["FirstName"]} {dr["LastName"]}");
}
await connection.CloseAsync();
#endregion

#region ORM
NorthwindDbContext context = new();
var employeeDatas = await context.Employees.ToListAsync();

#endregion

//ORM'in kullanılmadığı durumlarda nasıl bir yaklaşım sergilenir?
//ORM'li ve ORM'siz kod mukayesesi
//İlişkisel eşleştirme nedir?
//SQL'den arındırılmış kod!
//Neden ORM kullanmalıyız?