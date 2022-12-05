
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

ApplicationDbContext context = new();

#region Query Tags Nedir?
//EF Core ile generate edilen sorgulara açıklama eklememizi sağlayarak; SQL Profiler, Query Log vs. gibi yapılarda bu açıklamalar eşliğinde sorguları gözlemlememizi sağlayan bir özelltir.

//await context.Persons.ToListAsync();

#endregion
#region TagWith Metodu
//await context.Persons.TagWith("Örnek bir açıklama...").ToListAsync();
#endregion
#region Multiple TagWith
//await context.Persons.TagWith("Tüm personeller çekilmişit.r")
//    .Include(p => p.Orders).TagWith("Personellerin yaptığı satışlar sorguya eklenmiştir.")
//    .Where(p => p.Name.Contains("a")).TagWith("Adında 'a' harfi olan personeller filtrelenmiştir.")
//    .ToListAsync();
#endregion
#region TagWithCallSite Metodu
//Oluşturulan sorguya açıklama satırı ekler ve ek olarak bu sorgunun bu dosyada (.cs) hangi satırda üretildiğini bilgisini de verir.
await context.Persons.TagWithCallSite("Tüm personeller çekilmişit.r")
    .Include(p => p.Orders).TagWithCallSite("Personellerin yaptığı satışlar sorguya eklenmiştir.")
    .Where(p => p.Name.Contains("a")).TagWithCallSite("Adında 'a' harfi olan personeller filtrelenmiştir.")
    .ToListAsync();
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
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Person)
            .HasForeignKey(o => o.PersonId);
    }
    readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder
    .AddFilter((category, level) =>
    {
        return category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information;
    })
    .AddConsole());
    protected override async void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDB;User ID=SA;Password=1q2w3e4r+!;TrustServerCertificate=True");
        optionsBuilder.UseLoggerFactory(loggerFactory);
    }
}
