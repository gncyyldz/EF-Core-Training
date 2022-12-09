using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

ApplicationDbContext context = new();

#region Temporal Tables Nedir?
//Veri değişikliği süreçlerinde kayıtları depolayan ve zaman içinde farklı noktalardaki tablo verilerinin analizi için kullanılan ve sistem tarafından yönetilen tablolardır.
//EF Core 6.0 ile desteklenmektedir.
#endregion
#region Temporal Tables Özelliğiyle Nasıl Çalışılır?
//EF Core'daki migration yapıları sayesinde tempral table'lar oluşturulup veritabanında üretilebilmektedir.
//Mevcut tabloları migration'lar aracılığıyla Temporal Table'lara dönüştürülebilmektedir.
//Herhangi bir tablonun verisel olarak geçmişini rahatlıkla sorgulayabiliriz.
//Herhangi bir tablodaki bir verinin geçmişteki herhangi bir T anındaki hali/duırumu/verileri geri getirilebilmektedir.
#endregion
#region Temporal Table Nasıl Uygulanır?

#region IsTemoral Yapılandırması
//EF Core bu yapılandırma fonksiyonu sayesinde ilgili entity'e karşılık üretilecek tabloda temporal table oluşturacağını anlamaktadır. Yahut önceden ilgili tablo üretilmişse eğer onu temporal table'a dönüştürecektir.
#endregion
#region Temporal Table İçin Üretilen Migration'ın İncelenmesi

#endregion
#endregion
#region Temporal Table'ı Test Edelim

#region Veri Eklerken
//Temporal Table'a veri ekleme süreçlerinde herhangi bir kayıt atılmaz! Temporal Table'ın yapısı, var olan veirler üzerindeki zamansal değişimleri takip etmek üzerine kuruludur!
//var persons = new List<Person>() {
//    new(){ Name = "Gençay", Surname = "Yıldız", BirthDate = DateTime.UtcNow },
//    new(){ Name = "Mustafa", Surname = "Yıldız", BirthDate = DateTime.UtcNow },
//    new(){ Name = "Suzan", Surname = "Yıldız", BirthDate = DateTime.UtcNow },
//    new(){ Name = "Yarkın", Surname = "Yıldız", BirthDate = DateTime.UtcNow },
//    new(){ Name = "Şuayip", Surname = "Yıldız", BirthDate = DateTime.UtcNow },
//    new(){ Name = "Sebahattin", Surname = "Yıldız", BirthDate = DateTime.UtcNow }
//};

//await context.Persons.AddRangeAsync(persons);
//await context.SaveChangesAsync();
#endregion
#region Veri Güncellerken
//var person = await context.Persons.FindAsync(3);
//person.Name = "Ahmet";
//await context.SaveChangesAsync();
#endregion
#region Veri Silerken
//var person = await context.Persons.FindAsync(3);
//context.Persons.Remove(person);
//await context.SaveChangesAsync();
#endregion
#endregion
#region Temporal Table Üzerinden Geçmiş Verisel İzleri Sorgulama

#region TemporalAsOf
//Belirli bir zaman için değişikiğe uğrayan tüm öğeleri döndüren bir fonksiyondur.
//var datas = await context.Persons.TemporalAsOf(new DateTime(2022, 12, 09, 05, 30, 04)).Select(p => new
//{
//    p.Id,
//    p.Name,
//    PeriodStart = EF.Property<DateTime>(p, "PeriodStart"),
//    PeriodEnd = EF.Property<DateTime>(p, "PeriodEnd"),
//}).ToListAsync();

//foreach (var data in datas)
//{
//    Console.WriteLine(data.Id + " " + data.Name + " | " + data.PeriodStart + " - " + data.PeriodEnd);
//}
#endregion
#region TemporalAll
//Güncellenmiş yahut silinmiş olan tüm verilerin geçmiş sürümlerini veya geçerli durumlarını döndüren bir fonksiyondur.
//var datas = await context.Persons.TemporalAll().Select(p => new
//{
//    p.Id,
//    p.Name,
//    PeriodStart = EF.Property<DateTime>(p, "PeriodStart"),
//    PeriodEnd = EF.Property<DateTime>(p, "PeriodEnd"),
//}).ToListAsync();

//foreach (var data in datas)
//{
//    Console.WriteLine(data.Id + " " + data.Name + " | " + data.PeriodStart + " - " + data.PeriodEnd);
//}
#endregion
#region TemporalFromTo
//Belirli bir zaman aralığı içerisindelki verileri döndüren fonksiyondur. Başlangıç ve bitiş zamanı dahil değildir.
////Başlangıç : 2022-12-09 05:29:55.0953716
//var baslangic = new DateTime(2022, 12, 09, 05, 29, 55);
////Bitiş     : 2022-12-09 05:30:30.3459797
//var bitis = new DateTime(2022, 12, 09, 05, 30, 30);

//var datas = await context.Persons.TemporalFromTo(baslangic, bitis).Select(p => new
//{
//    p.Id,
//    p.Name,
//    PeriodStart = EF.Property<DateTime>(p, "PeriodStart"),
//    PeriodEnd = EF.Property<DateTime>(p, "PeriodEnd"),
//}).ToListAsync();

//foreach (var data in datas)
//{
//    Console.WriteLine(data.Id + " " + data.Name + " | " + data.PeriodStart + " - " + data.PeriodEnd);
//}
#endregion
#region TemporalBetween
////Belirli bir zaman aralığı içerisindelki verileri döndüren fonksiyondur. Başlangıç verisi dahil değil ve bitiş zamanı ise dahildir.
////Başlangıç : 2022-12-09 05:29:55.0953716
//var baslangic = new DateTime(2022, 12, 09, 05, 29, 55);
////Bitiş     : 2022-12-09 05:30:30.3459797
//var bitis = new DateTime(2022, 12, 09, 05, 30, 30);

//var datas = await context.Persons.TemporalBetween(baslangic, bitis).Select(p => new
//{
//    p.Id,
//    p.Name,
//    PeriodStart = EF.Property<DateTime>(p, "PeriodStart"),
//    PeriodEnd = EF.Property<DateTime>(p, "PeriodEnd"),
//}).ToListAsync();

//foreach (var data in datas)
//{
//    Console.WriteLine(data.Id + " " + data.Name + " | " + data.PeriodStart + " - " + data.PeriodEnd);
//}
#endregion
#region TemporalContainedIn
////Belirli bir zaman aralığı içerisindelki verileri döndüren fonksiyondur. Başlangıç ve bitiş zamanı ise dahildir.
////Başlangıç : 2022-12-09 05:29:55.0953716
//var baslangic = new DateTime(2022, 12, 09, 05, 29, 55);
////Bitiş     : 2022-12-09 05:30:30.3459797
//var bitis = new DateTime(2022, 12, 09, 05, 30, 30);

//var datas = await context.Persons.TemporalContainedIn(baslangic, bitis).Select(p => new
//{
//    p.Id,
//    p.Name,
//    PeriodStart = EF.Property<DateTime>(p, "PeriodStart"),
//    PeriodEnd = EF.Property<DateTime>(p, "PeriodEnd"),
//}).ToListAsync();

//foreach (var data in datas)
//{
//    Console.WriteLine(data.Id + " " + data.Name + " | " + data.PeriodStart + " - " + data.PeriodEnd);
//}
#endregion
#endregion
#region Silinmiş Bir Veriyi Temporal Table'dan Geri Getirme
//Silinmiş bir veriyi temporal table'dan getirebilmek için öncelikle yapılması gerekenb ilgili verinin silindiği tarihi bulmamız gerekmektedir. Ardından TemporalAsOf fonksiyonu ile silğinen verinin geçmiş değeri elde edilebilir ve fizilse tabloya bu veri taşınabilir.

//Silindiği tarih
var dateOfDelete = await context.Persons.TemporalAll()
    .Where(p => p.Id == 3)
    .OrderByDescending(p => EF.Property<DateTime>(p, "PeriodEnd"))
    .Select(p => EF.Property<DateTime>(p, "PeriodEnd"))
    .FirstAsync();

var deletedPerson = await context.Persons.TemporalAsOf(dateOfDelete.AddMilliseconds(-1))
    .FirstOrDefaultAsync(p => p.Id == 3);

await context.AddAsync(deletedPerson);

await context.Database.OpenConnectionAsync();

await context.Database.ExecuteSqlInterpolatedAsync($"SET IDENTITY_INSERT dbo.Persons ON");
await context.SaveChangesAsync();
await context.Database.ExecuteSqlInterpolatedAsync($"SET IDENTITY_INSERT dbo.Persons OFF");

#region SET IDENTITY_INSERT Konfigürasyonu
//Id ile veri ekleme sürecinde ilgili verinin id sütununa kayıt işleyebilmek için veriyi fiziksel tabloya taşıma işleminden önce veritabanı seviyesinde SET IDENTITY_INSERT komutu eşliğinde id bazlı veri ekleme işlemi aktifleştirilmelidir.
#endregion
#endregion

class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; }
}
class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Employee> Employees { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().ToTable("Employees", builder => builder.IsTemporal());
        modelBuilder.Entity<Person>().ToTable("Persons", builder => builder.IsTemporal());
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDB;User ID=SA;Password=1q2w3e4r+!;TrustServerCertificate=True");
    }
}