using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

ApplicationDbContext context = new();
#region Scalar Functions Nedir?
//Geriye herhangi bir türde değer döndüren fonksiyonlardır.
#endregion
#region Scalar Function Oluşturma
//1. adım : boş bir migration oluşturulmalı.
//2. adım : bu migration içerisinde Up metodunda Sql metodu eşliğinde fonksiyonun create kodları yazılacak, Down metodu içerisinde de Drop kodları yazılacaktır.
//3. adım : migrate edilmeli.
#endregion
#region Scalar Function'ı EF Core'a Entegre Etme

#region HasDbFunction
//Veritabanı seviyesindeki herhangi bir fonksiyonu EF Core/yazılım kısmında bir metoda bind etmemizi sağlayan fonksiyondur.
#endregion

//var persons = await (from person in context.Persons
//                     where context.GetPersonTotalOrderPrice(person.PersonId) > 500
//                     select person).ToListAsync();

//Console.WriteLine();

#endregion

#region Inline Functions Nedir?
//Geriye bir değer değil, tablo döndüren fonksiyonlardır.
#endregion
#region Inline Function Oluşturma
//1. adım : boş bir migration oluşturunuz.
//2. adım : bu migration içerisindeki Up fonksiyonunda Create işlemini,  down fonksiyonunda ise drop işlemlerini gerçekleştiriniz.
//3. adım : migrate ediniz.
#endregion
#region Inline Function'ı EF Core'a Entegre Etme
var persons = await context.BestSellingStaff(3000).ToListAsync();
foreach (var person in persons)
{
    Console.WriteLine($"Name : {person.Name} | Order Count : {person.OrderCount} | Total Order Price : {person.TotalOrderPrice}");
}
#endregion

public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }

    public ICollection<Order> Orders { get; set; }
}
public class Order
{
    public int OrderId { get; set; }
    public int PersonId { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }

    public Person Person { get; set; }
}
public class BestSellingStaff
{
    public string Name { get; set; }
    public int OrderCount { get; set; }
    public int TotalOrderPrice { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Scalar
        modelBuilder.HasDbFunction(typeof(ApplicationDbContext).GetMethod(nameof(ApplicationDbContext.GetPersonTotalOrderPrice), new[] { typeof(int) }))
            .HasName("getPersonTotalOrderPrice");
        #endregion
        #region Inline
        modelBuilder.HasDbFunction(typeof(ApplicationDbContext).GetMethod(nameof(ApplicationDbContext.BestSellingStaff), new[] { typeof(int) }))
            .HasName("bestSellingStaff");

        modelBuilder.Entity<BestSellingStaff>()
            .HasNoKey();
        #endregion

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Person)
            .HasForeignKey(o => o.PersonId);
    }


    #region Scalar Functions
    public int GetPersonTotalOrderPrice(int personId)
        => throw new Exception();
    #endregion
    #region Inline Functions
    public IQueryable<BestSellingStaff> BestSellingStaff(int totalOrderPrice = 10000)
         => FromExpression(() => BestSellingStaff(totalOrderPrice));
    #endregion


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDB;User ID=SA;Password=1q2w3e4r+!;TrustServerCertificate=True");
    }
}