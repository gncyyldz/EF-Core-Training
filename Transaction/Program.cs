
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;
using System.Reflection.Metadata;
using System.Transactions;

ApplicationDbContext context = new();
#region Transaction Nedir?
//Transaction, veritaanındaki kümülatif işlemleri atomik bir şekilde gerçekleştirmemizi sağlayan bir özelliktir.
//Bir transaction içerisindkei tüm işlemler commit edildiği taktirde veritabanına fiziksel olarak yansıtılacaktır. Ya da rollback edilirse tüm işlemler geri alınacak ve fiziksel olarak veritabanında herhangi bir verisel değişiklik durumu söz konusu olmayacaktır.
//Transaction'ın genel amacı veritabanındaki tutarlılık durumunu korumaktadır. Ya da bir başka deyişle verityabanındaki tutarsızlık durumlarına karşı önlem almaktır.
#endregion
#region Default Transaction Davranışı
//EF Core'da varsayılan olarak, yapılan tüm işlemler SaveChanges fonksiyuyla veritabanına fiziksel olarak uygulanır. 
//Çünkü SaveChanges default olarak bir trasncationa sahiptir.
//Eğer ki bu süreçte bir problem/hata/başarısızlık durumu söz konusu olursa tüm işlemler geri alınır(rollback) ve işlemlerin hiçbiri veritabanına uygulanmaz.
//Böylece SaveChanges tüm işlemlerin ya tamamen başarılı olacağını ya da bir hata oluşursa veritabanını değiştirmeden işlemleri sonlandıracağını ifade etmektedir.
#endregion
#region Transaction Kontrolünü Manuel Sağlama
//IDbContextTransaction transaction = await context.Database.BeginTransactionAsync();
//EF Core'da transaction kontrolü iradeli bir şekilde manuel sağlamak yani elde etmek istiyorsak eğer BeginTransactionAsync fonksiyonu çağrılmalıdır.

//Person p = new() { Name = "Abuzer" };
//await context.Persons.AddAsync(p);
//await context.SaveChangesAsync();

//await transaction.CommitAsync();
#endregion
#region Savepoints
//EF Core 5.0 sürümüyle gelmiştir.
//Savepoints, veritabanıu işlemleri sürecinde bir hata oluşursa veya başka bir nedenle yapılan işlemlerin geri alınması gerekiyorsa transaciton içerisinde dönüş yapılabilecek noktaları ifade eden bir özelliktir.
#region CreateSavepoint
//Transaction içerisinde geri dönüş noktası oluşturmamızı sağlayan bir fonksiyondur.
#endregion
#region RollbackToSavepoint
//Transacction içerisinde herhangi bir geri dönüş noktasına(Savepoint'e) rollback yapmamızı sağlayan fonksiyondur.
#endregion

//Savepoints özelliği bir transaction içerisinde istenildiği kadar kullanılabilir.

//IDbContextTransaction transaction = await context.Database.BeginTransactionAsync();

//Person p13 = await context.Persons.FindAsync(13);
//Person p11 = await context.Persons.FindAsync(11);
//context.Persons.RemoveRange(p13, p11);
//await context.SaveChangesAsync();

//await transaction.CreateSavepointAsync("t1");

//Person p10 = await context.Persons.FindAsync(10);
//context.Persons.Remove(p10);
//await context.SaveChangesAsync();

//await transaction.RollbackToSavepointAsync("t1");

//await transaction.CommitAsync();
#endregion
#region TransactionScope  
//veritabanı işlemlerini bir grup olarak yapmamızı sağlayan bir sınıfıtr.
//ADO.NET ile de kullanılabilir.

//using TransactionScope transactionScope = new();
//Veritabanı işlemleri...
//..
//..
//transactionScope.Complete(); //Compote fonksiyonu yapılan veritabanı işlemlerinin commit edilmesini sağlar.
//Eğer ki rollback yapacaksanız complete fonksiyonunun tetiklenmemesi yeterlidir!

#region Complete

#endregion
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