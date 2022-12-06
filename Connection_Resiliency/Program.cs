using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Reflection;

ApplicationDbContext context = new();

#region Connection Resiliency Nedir?
//Başarısız olan veritabanı komutlarını otomatik olarak tekrar deneyen bir özelliktir.
var p = context.Persons.ToList();
foreach (var person in p)
{
    Console.WriteLine(person.Name);
}
#endregion
#region EnableRetryOnFailure

#region MaxRetryCount

#endregion
#region MaxRetryDelay

#endregion
#endregion

#region Execution Strategies



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

    protected override async void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDB;User ID=SA;Password=1q2w3e4r+!;TrustServerCertificate=True", options => options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), new[] { 4060 }))
            .LogTo(filter: (eventId, level) => eventId.Id == CoreEventId.ExecutionStrategyRetrying,
            logger: eventData =>
            {
                Console.WriteLine("Yeniden bağlantı deneniyor...");
            });
    }
}
