using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

ApplicationDbContext context = new();

//Eğer ki, sorgunuzu LINQ ile ifade edemiyorsanız yahut lINQ'in ürettiği sorguya nazaran daha optimize bir sorguyu manuel geliştirmek ve EF Core üzerinden execute etmek istiyorsanız EF Core'un bu davrnaışı desteklediğini bilmelisiniz.

//Manuel bir şekilde/tarafımızca oluşturulmuş olan sorguları EF Core tarafından execute edebilmek için o sorgunun sonucunu karşılayacak bir entity model'ın tasarlanmış ve bunun DbSet olarak context nesnesine tanımlanmış olması gerekiyor.
#region FromSqlInterpolated
//EF Core 7.0 sürümünden önce ham sorguları execute edebildiğimiz fonksiyondur.
//var persons = await context.Persons.FromSqlInterpolated($"SELECT * FROM Persons")
//    .ToListAsync();
#endregion

#region FromSql - EF Core 7.0
//EF Core 7.0 ile gelen metottur. 

#region Query Execute
//var persons = await context.Persons.FromSql($"SELECT * FROM Persons")
//.ToListAsync();
#endregion
#region Stored Procedure Execute
//var persons = await context.Persons.FromSql($"EXECUTE dbo.sp_GetAllPersons 4")
//    .ToListAsync();
#endregion
#region Parametreli Sorgu Oluşturma
#region Örnek 1
//int personId = 3;
//var persons = await context.Persons.FromSql($"SELECT * FROM Persons Where PersonId = {personId}")
//    .ToListAsync();

//Burada sorguya geçirilen personId değişkeni arkaplanda bir DbParameter türüne dönüştürülerek o şekilde sorguya dahil edilmektedir.
#endregion
#region Örnek 2
//int personId = 3;
//var persons = await context.Persons.FromSql($"EXECUTE dbo.sp_GetAllPersons {personId}")
//    .ToListAsync();
#endregion
#region Örnek 3
//SqlParameter personId = new("PersonId", "3");
//personId.DbType = System.Data.DbType.Int32;
//personId.Direction = System.Data.ParameterDirection.Input;

//var persons = await context.Persons.FromSql($"SELECT * FROM Persons Where PersonId = {personId}")
//    .ToListAsync();
#endregion
#region Örnek 4
//SqlParameter personId = new("PersonId", "3");
//var persons = await context.Persons.FromSql($"EXECUTE dbo.sp_GetAllPersons {personId}")
//    .ToListAsync();
#endregion
#region Örnek 5
//SqlParameter personId = new("PersonId", "3");
//var persons = await context.Persons.FromSql($"EXECUTE dbo.sp_GetAllPersons @PersonId = {personId}")
//    .ToListAsync();
#endregion
#endregion
#endregion

#region Dynamic SQL Oluşturma ve Parametre Girme - FromSqlRaw
//string columnName = "PersonId", value = "3";
//var persons = await context.Persons.FromSql($"Select * From Persons Where {columnName} = {value}")
//    .ToListAsync();

//EF Core dinamik olarak oluşturulan sorgularda özellikle kolon isimleri parametreleştirilmişse o sorguyu ÇALIŞTIRMAYACAKTIR!

//string columnName = "PersonId";
//SqlParameter value = new("PersonId", "3");
//var persons = await context.Persons.FromSqlRaw($"Select * From Persons Where {columnName} = @PersonId", value)
//    .ToListAsync();

//FromSql ve FromSqlInterpolated metotlarında SQL Injection vs. gibi güvenlik önlemleri alınmış vaziyettedir. Lakin dinamik olarak sorguları oluşturuyorsanız eğer burada güvenlik geliştirici sorumludur. Yani gelen sorguda/veri yorumlar, noktalı virgüller yahut SQL'e özel karakterlerin algılanması ve bunların temizlenmesi geliştirici tarafından gerekmektedir.
#endregion
#region SqlQuery - Entiy Olmayan Scalar Sorguların Çalıştırılması - Non Entity - EF Core 7.0
//Entity'si olmayan scalar sorguların çalıştırılıp sonucunu elde etmemizi sağlayan yeni bir fonksiyondur.
//var data = await context.Database.SqlQuery<int>($"SELECT PersonId FROM Persons")
//    .ToListAsync();

//var persons = await context.Persons.FromSql($"SELECT * FROM Persons")
//    .Where(p => p.Name.Contains("a"))
//    .ToListAsync();

//var data = await context.Database.SqlQuery<int>($"SELECT PersonId Value FROM Persons")
//    .Where(x => x > 5)
//    .ToListAsync();

//SqlQuery'de LINQ operatörleriyle sorguya ekstradan katkıda bulunmak istiyorsanız eğer bu sorgu neticesinde gelecek olan kolonun adını Value olarak bildirmeniz gerekmektedir. Çünkü, SqlQuery metodu sorguyu bir subquery olarak generate etmektedir. Haliyle bu durumdan dolayı LINQ ile verilen şart ifadeleri statik olarka Value kolonuna göre tasarlanmıştır. O yüzden bu şekilde bir çalışma zorunlu gerekmektedir.

#endregion
#region ExecuteSql
//Insert, update, delete
//await context.Database.ExecuteSqlAsync($"Update Persons SET Name = 'Fatma' WHERE PersonId = 1");
#endregion
#region Sınırlılıklar
//Queryler entity türünün tüm özellikleri için kolonlarda değer döndürmelidir.
//var persons = await context.Persons.FromSql($"SELECT Name, PersonId FROM Persons")
//    .ToListAsync();

//Sütun isimleri proıperty isimleriyle aynı olmalıdır.

//SQL Sorgusu Join yapısı İÇEREMEZ!!! Haliyle bu tarz ihtiyaç noktalarında Include fonksiyonu KULLANILMALIDIR!
//var persons = await context.Persons.FromSql($"SELECT * FROM Persons")
//    .Include(p => p.Orders)
//    .ToListAsync();
#endregion
Console.WriteLine();
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
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDB;User ID=SA;Password=1q2w3e4r+!;TrustServerCertificate=True");
    }
}


//CREATE PROC sp_GetAllPersons
//(
//	@PersonId INT NULL
//)AS
//BEGIN
//	IF @PersonId IS NULL
//		SELECT * FROM Persons
//	ELSE
//		SELECT * FROM Persons WHERE PersonId = @PersonId
//END
