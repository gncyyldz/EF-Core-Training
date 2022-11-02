using Microsoft.EntityFrameworkCore;
using System.Reflection;
ApplicationDbContext context = new();
#region Explicit Loading

//Oluşturulan sorguya eklenecek verilerin şartlara bağlı bir şekilde/ihtiyaçlara istinaden yüklenmesini sağlayan bir yaklaşımdır.

//var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);
//if (employee.Name == "Gençay")
//{
//    var orders = await context.Orders.Where(o => o.EmployeeId == employee.Id).ToListAsync();
//}

#region Reference

//Explicit Loading sürecinde ilişkisel olarak sorguya eklenmek istenen tablonun navigation propertysi eğer ki tekil bir türse bu tabloyu reference ile sorguya ekleyebilemkteyiz.

//var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);
////...
////...
////...
//await context.Entry(employee).Reference(e => e.Region).LoadAsync();

//Console.WriteLine();
#endregion
#region Collection

//Explicit Loading sürecinde ilişkisel olarak sorguya eklenmek istenen tablonun navigation propertysi eğer ki çoğul/koleksiyonel bir türse bu tabloyu Collection ile sorguya ekleyebilemkteyiz.

//var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);
//...
//...
//...
//await context.Entry(employee).Collection(e => e.Orders).LoadAsync();

//Console.WriteLine();
#endregion

#region Collection'lar da Aggregate Operatör Uygulamak
//var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);
//...
//...
//...
//var count = await context.Entry(employee).Collection(e => e.Orders).Query().CountAsync();
Console.WriteLine();
#endregion
#region Collection'lar da Filtreleme Gerçekleştirmek
//var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);
////...
////...
////...
//var orders = await context.Entry(employee).Collection(e => e.Orders).Query().Where(q => q.OrderDate.Day == DateTime.Now.Day).ToListAsync();
#endregion
#endregion

public class Employee
{
    public int Id { get; set; }
    public int RegionId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Salary { get; set; }

    public List<Order> Orders { get; set; }
    public Region Region { get; set; }
}
public class Region
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Employee> Employees { get; set; }
}
public class Order
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime OrderDate { get; set; }

    public Employee Employee { get; set; }
}


class ApplicationDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Region> Regions { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDB;User ID=SA;Password=1q2w3e4r+!;TrustServerCertificate=True");
    }
}