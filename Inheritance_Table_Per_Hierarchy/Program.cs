using Microsoft.EntityFrameworkCore;
ApplicationDbContext context = new();
#region Table Per Hierarchy (TPH) Nedir?
//Kalıtımsal ilişkiye sahip olan entitylerin olduğu senaryolarda her bir hiyerarşiye karşılık bir tablo oluşturan davranıştır.
#endregion
#region Neden Table Per Hierarchy Yaklaşımında Bir Tabloya İhtiyacımız Olsun?
//İÇerisinde benzer alanlara sahip olan entityleri migrate ettiğimizde her entitye karşılık bir tablo oluşturmaktansa bu entityleri tek bir tabloda modellemek isteyebilir ve bu tablodaki kayıtları discriminator kolonu üzerinden birbirlerinden ayırabiliriz. İşte bu tarz bir tablonun oluşturulması ve bu tarz bir tabloya göre sorgulama, veri ekleme, silme vs. gibi operasyonların şekillendirilmesi için TPH davranışını kullanabiliriz.
#endregion
#region TPH Nasıl Uygulanır?
//EF Core'da entity aransında temel bir kalıtımsal ilişki söz konusuysa eğer default oalrak kabul edilen davranıştır.
//O yüzden herhangi bir konfigüreasyon gerektirmez!
//Entityler kendi aralarında kalıtımsal ilişkiye sahip olmalı ve bu entitylerin hepsi DbContext nesnesine DbSet olarak eklenmelidir! 
#endregion
#region Discriminator Kolonu Nedir?
//Table Per Hierarchy yaklaşımı neticesinde kümülatif olarak inşa edilmiş tablonun hangi entitye karşılık veri tuttuğunu ayırt edebilmemizi sağlayan bir kolondur.
//EF Core tarafından otomatik olarak tabloya yerleştirilir.
//Default olarak içerisinde entity isimlerini tutar.
//Discriminator kolonunu komple özelleştirebiliriz.
#endregion
#region Discriminator Kolon Adı Nasıl Değiştirilir?
//Öncelikle hiyerarşinin başında hangi sınıf varsa onun Fluent API'da konfigürasyonuna gidilmeli.
//Ardından HasDiscriminator fonksiyonu ile özelleştirilmeli.
#endregion
#region Discriminator Değerleri Nasıl Değiştirilir?
//Yine hiyearşinin bşaındaki entity konfigürasyonlarına gelip, HasDiscriminator fonksiyonu ile özelleştirmede bulunarak ardından HasValue fonksiyonu ile hangi entitye karşılık hangi değerin girieceğini belirtilen türde ifade edebilirsiniz.
#endregion
#region TPH'da Veri Ekleme
//Davranışların hiçbirinde veri eklerken,silerken, güncellerken vs. normal operasyonların dışında bir işlem yapılmaz!
//Hangi davranışıo kullanıyorsanız EF Core ona göre arkaplanda modellemeyi gerçekleştirecektir.
//Employee e1 = new() { Name = "Gençay", Surname = "Yıldız", Department = "Yazılım Bilgi İşlem" };
//Employee e2 = new() { Name = "Nevin", Surname = "Yıldız", Department = "Yazılım Bilgi İşlem" };
//Customer c1 = new() { Name = "Ahmet", Surname = "Bilmemne", CompanyName = "Ahmet Bilmemne Halı Kilim Yıkama" };
//Customer c2 = new() { Name = "Şuayip", Surname = "XYZ", CompanyName = "Şuayip Sucuk" };
//Technician t1 = new() { Name = "Rıfkı", Surname = "Kıllıbacak", Department = "Muhasebe", Branch = "Şöför" };
//await context.Employees.AddAsync(e1);
//await context.Employees.AddAsync(e2);
//await context.Customers.AddAsync(c1);
//await context.Customers.AddAsync(c2);
//await context.Technicians.AddAsync(t1);

//await context.SaveChangesAsync();
#endregion
#region TPH'da Veri Silme
//TPH davranışında silme operasyonu yine entity üzerinden gerçekleştirilir.
//var employee = await context.Employees.FindAsync(1);
//context.Employees.Remove(employee);
//await context.SaveChangesAsync();

//var customers = await context.Customers.ToListAsync();
//context.Customers.RemoveRange(customers);
//await context.SaveChangesAsync();
#endregion
#region TPH'da Veri Güncelleme
//TPH davranışında güncelleme operasyonu yine entity üzerinden gerçekleştirilir.
//Employee guncellenecek = await context.Employees.FindAsync(8);
//guncellenecek.Name = "Hilmi";
//await context.SaveChangesAsync();
#endregion
#region TPH'da Veri Sorgulama
//Veri sorgulama oeprasyonu bilinen DbSet propertysi üzerinden sorgulamadır. Ancak burada dikkat edilmesi gereken bir husus vardır. O da şu;
//var employees = await context.Employees.ToListAsync();
//var techs = await context.Technicians.ToListAsync();
//kalıtımsal ilişkiye göre yapılan sorgulamada üst sınıf alt sınıftaki verileride kapsamaktadır. O yüzden üst sınıfların sorgulamalarında alt sınıfların verileride gelecektir buna dikkat edilmelidir.
//Sorgulama süreçlerinde EF Core generate edilen sorguya bir where şartı eklemektedir.
#endregion
#region Farklı Entity'ler de Aynı İsimde Sütunların Olduğu Durumlar
//Entitylerde mükerrer kolonlar olabilir. Bu kolonları EF core isimsel olarak özelleştirip ayıracaktır.
#endregion
abstract class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}
class Employee : Person
{
    public string? Department { get; set; }
}
class Customer : Person
{
    public int A { get; set; }
    public string? CompanyName { get; set; }
}
class Technician : Employee
{
    public int A { get; set; }
    public string? Branch { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Technician> Technicians { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Person>()
        //    .HasDiscriminator<string>("ayirici")
        //    .HasValue<Person>("A")
        //    .HasValue<Employee>("B")
        //    .HasValue<Customer>("C")
        //    .HasValue<Technician>("D");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDB;User ID=SA;Password=1q2w3e4r+!;TrustServerCertificate=True");
    }
}