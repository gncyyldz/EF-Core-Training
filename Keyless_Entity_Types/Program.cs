using Microsoft.EntityFrameworkCore;
using System.Reflection;

ApplicationDbContext context = new();


#region Keyless Entity Types
//Normal entity type'lara ek olarak primary key içermeyen querylere karşı veritabanı sorguları yürütmek için kullanılan bir özelliktir KET.

//Genellikle aggreate operasyonların yapıldığı group by yahut pivot table gibi işlemler neticesinde elde edilen istatistiksel sonuçlar primary key kolonu barındırmazlar. Bizler bu tarz sorguları Keyless Entity Types özelliği ile sanki bir entity'e karşılı geliyormuş gibi çalıştırabiliriz.
#endregion

#region Keyless Entity Types Tanımlama
//1. Hangi sorgu olursa olsun EF Core üzerinden bu sorgunun bir entity'e karşılık geliyormuş gibi işleme/execute'a/çalıştırmaya tabi tutulabilmesi için o sorgunun sonucunda bir entity'nin yine de tasarlanması gerekmektedir.
//2. Bu entity'nin DbSet property'si olarak DbContext nesnesine eklenmesi gerekmektedir.
//3. Tanımlamış olduğumuz entity'e OnModelCreating fonksiyonu içerisinde girip bunun bir key'i olmadığını bildirmeli (HasNoKey) ve hangi sorgunun çalıştırılacağı da ToView vs. gibi işlemlerle ifade edilmelidir.

//var datas = await context.PersonOrderCounts.ToListAsync();
//Console.WriteLine();
#region Keyless Attribute'u

#endregion
#region HasNoKey Fluent API'ı

#endregion
#endregion
#region Keyless Entity Types Özellikleri Nelerdir?
//Primary Key kolonu OLMAZ!
//Change Tracker mekanizması aktif olmayacaktır.
//TPH olarak entity hiyerarşisinde kullanılabilir lakin diğer kalıtımsal ilişkilerde kullanılamaz!
#endregion

[Keyless]
public class PersonOrderCount
{
    public string Name { get; set; }
    public int Count { get; set; }
}
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
    public DbSet<PersonOrderCount> PersonOrderCounts { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Person)
            .HasForeignKey(o => o.PersonId);

        modelBuilder.Entity<PersonOrderCount>()
            .HasNoKey()
            .ToView("vw_PersonOrderCount");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDB;User ID=SA;Password=1q2w3e4r+!;TrustServerCertificate=True");
    }
}