using Microsoft.EntityFrameworkCore;
using System.Reflection;

ApplicationDbContext context = new();
#region View Nedir?
//Oluşturduğumuz kompleks sorguları ihtiyaç durumlarında daha rahat bir şekilde kullanabilmek için basitleştiren bir veritabanı objesidir.
#endregion
#region EF Core İle View Kullanımı

#region View Oluşturma
//1. adım : boş bir migration oluşturulmalıdır.
//2. adım : migration içerisindeki Up fonksiyonunda view'in create komutları, down fonksiyonunda ise drop komutları yazılmalıdır.
//3. adım : migrate ediniz.
#endregion
#region View'i DbSet Olarak Ayarlama
//View'i EF Core üzerinden sorgulayabilmek için view neticesini karşılayabilecek bir entity olşturulması ve bu entity türünden DbSet property'sinin eklenmesi gerekmektedir.
#endregion
#region DbSet'in Bir View Olduğunu Bildirmek

#endregion

//var personOrders = await context.PersonOrders
//    .Where(po => po.Count > 10)
//    .ToListAsync();

#region EF Core'da View'lerin Özellikleri
//Viewlerde primary key olmaz! Bu yüzden ilgili DbSet'in HasNoKey ile işaretlenmesi gerekemktedir.
//View neticesinde gelen veriler Change Tracker ile takip edilmezler. Haliyle üzerlerinde yapılan değişiklikleri EF Core veritabanına yansıtmaz

//var personOrder = await context.PersonOrders.FirstAsync();
//personOrder.Name = "Abuzer";
//await context.SaveChangesAsync();
#endregion
Console.WriteLine();
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

    public Person Person { get; set; }
}
public class PersonOrder
{
    public string Name { get; set; }
    public int Count { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<PersonOrder> PersonOrders { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<PersonOrder>()
            .ToView("vm_PersonOrders")
            .HasNoKey();

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Person)
            .HasForeignKey(o => o.PersonId);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDB;User ID=SA;Password=1q2w3e4r+!;TrustServerCertificate=True");
    }
}