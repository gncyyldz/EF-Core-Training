using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
ApplicationDbContext context = new();

#region Data Concurrency Nedir?
//Geliştirdiğimiz uygulamalarda ister istemez verisel olarak tutarsızlıklar meydana gelebilmektedir. Örneğin; birden fazla uygulamanın yahut client'ın aynı veritabanı üzerinde eşzamanı olarak çalıltığı durumlarda verisel anlamda uyuglamadan uygulamaya yahut client'tan clienta tutarsızlıklar meydana gelebilir.
//Data Concurrency kavramı, uygulamalardaki veri tutarsızlığı durumlarına karşılık yönetilebilirliği sağlayacak olan davranışları kapsayan bir kavramdır.

//Bir uygulamada veri tutarsızlığının olması demek o uygulamayı kullanan kullanıcıları yanıltmak demektir.
//Veri tutarsızlığının olduğu uygulamalarda istatistiksel olarak yanlış sonuçlar elde edilebilir...
#endregion
#region Stale & Dirty (Bayat & Kirli) Data Nedir?
//Stale Data : Veri tutarsızlığına sebebiyet verebilecek güncellenmemiş yahut zamanı geçmiş olan verileri ifade etmektedir. Örneğin; bir ürünün stok durumu sıfırlandığı halde arayüz üzerinde bunu ifade eden bir güncelleme durumu söz konusu değilse işte bu stale data durumuna bir örnektir.

//Dirty Data : Veri tutarszılığına sebebiyet verebilecek verinin hatalı yahut yanlış olduğunu ifade etmektedir. Örneğin; adı 'Ahmet' olan bir kullanıcının veritabanında 'Mehmet' olarak tutulması dirty data örneklendirmesidir.
#endregion
#region Last In Wins (Son Gelen Kazanır)
//Bir veri yapısında son yapılan aksiyona göre en güncel verinin en üstte bulunmasını/varlığını korumasını ifade eden bir deyimsel terimdir.
#endregion
#region Pessimistic Lock (Kötümser Kilitleme)

//Bir transaction sürecinde elde edilen veriler üzerinde farklı sorgularla değişiklik yapılmasını engellemek için ilgil iverilerin kitlenmesini(locking) sağlayarak değişikliğe karşı direnç oluşturulmasını ifade eden bir yöntemdir.

//Bu verilerin kilitlenmesi durumu ilgili transaction'ın commit ya da rollback edilmesi ile sınırlıdır.

#region Deadlock Nedir?
//Kitlenmiş olan bir verinin veirtabanı seviyesinde meydana gelen sistemsel bir hatadan dolayı kilidinin çözülememesi yahut döngüsel olarak kilitlenme durumunun meydana gelmesini ifade eden bir terimdir.

//Pessimistic Lock yönteminde deadlock durumunu yaşamanız bir ihtimaldir. O yüzden değerlendirlmesi gereken ve iyi düşünülerek tercih edilmesi gerken bir yaklaşımdır pessimistic lock yaklaşımı.
#endregion
#region  Kilitleme Çıkmazı - Ölüm Kilitlenmesi Nedir?

#endregion
#region WITH (XLOCK)
//using var transaction = await context.Database.BeginTransactionAsync();
//var data = await context.Persons.FromSql($"SELECT * FROM Persons WITH (XLOCK) WHERE PersonID = 5")
//    .ToListAsync();
//Console.WriteLine();
//await transaction.CommitAsync();
#endregion
#endregion
#region Optimistic Lock (İyimser Kilitmele)

//Bir verinin stale olup olmadığını anlamak için herhangi bir locking işlemi olmaksızın versiyon mantığıonda çalışmamızı sağlayan yaklaşımdır.
//Optimistic lock yönteminde, Pessimistic lock'da olduğu gibi veriler üzerinde tutarsızlığa mahal olabilecek değişiklikler fiziksel olarka engellenmemektedir. Yani veriler tutarsızlığı sağlayacak şekilde değiştirilebilir. 
//Amma velakin Optimistic lock yaklaşımı ile bu veriler üzerindeki tutarsızlık durumunu takip edebilmek için versiyon bilgisini kullanıyoruz. Bunu da şöyle kullanıyoruz;
//Her bir veriye karşılık bir versiyon bilgisi üretiliyor. Bu bilgi ister metinsel istersekte sayısal olabilir. Bu versiyon bilgisi veri üzerinde yapılan her bir değişiklik neticesinde güncellenecektir. Dolayısıyla bu güncellemeyi daha kolay bir şekild egerçkeleştirebilmek için sayısal olmasını tercih ederiz. 
//EF Core üzerinden verileri sorgularken ilgili verilerin versiyon bilgilerini de in-memory'e alıyoruz. Ardından veri üzerinde bir değişiklik yapılırsa eğer bu  inmemory'deki versiyon bilgisi ile verityabanındaki versiyon bilgisini karşılaştıroyruz. Eğer ki bu karşılaştırma doğrulanıyorsa yapılan aksiyon geçerli olacaktır, yok eğer doğrulanmıyorsa demek ki verinin değeri değişmiş anlamına gelecek yani bir tutarsızlık durumu olduğu anlaşılacaktır. İşte bu durumda bir hata fırlatılacak ve aksiyon gerçekleştirilmeyecektir.

//EF Core Optimistic lock yaklaşımı için genetinde yapısal bir özellik barındırmaktadır.

#region Property Based Configuration (ConcurrencyCheck Attribute)
//Verisel tutarlılığın kontrol edilmek istendiği proeprtyler ConcurrencyCheck attribute'u ile işaretlenir. Bu işaretleme neticesinde her bir entity'nin instance'ı için in-memory'de bir token değeri üretilecektir. Üretilen bu token değeri alınan aksiyon süreçlerinde EF Core tarafından doğrulacnacak ve eğer ki herhangi bir değişiklik yoksa aksiyon başarıyla sonlandırılmış olacaktır. Yok eğer transaction sürecinde ilgili veri üzerinde(ConcurrencyCheck attribute ile işaretlenmiş propertylerde) herhangi  bir değişiklik durumu söz konusuysa o taktirde üretilen token'da değiştirilecek ve haliyle doğrulama sürecinde geçerli olmayacağı anlaşılacağı için veri tutarsızlığı durumu olduğu anlaşılacak ve hata fırlatılacaktır.

//var person = await context.Persons.FindAsync(3);
//context.Entry(person).State = EntityState.Modified;
//await context.SaveChangesAsync();

#endregion
#region RowVersion Column
//Bu yaklaşımda ise veritabanındaki her bir satıra karşılık versiyon bilgisi fiziksel olarka oluşturulmaktadır.
//var person = await context.Persons.FindAsync(3);
//context.Entry(person).State = EntityState.Modified;
//await context.SaveChangesAsync();
#endregion
#endregion

public class Person
{
    public int PersonId { get; set; }
    //[ConcurrencyCheck]
    public string Name { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //modelBuilder.Entity<Person>().Property(p => p.Name).IsConcurrencyToken();
        modelBuilder.Entity<Person>().Property(p => p.RowVersion).IsRowVersion();
    }
    readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDB;User ID=SA;Password=1q2w3e4r+!;TrustServerCertificate=True")
            .UseLoggerFactory(_loggerFactory);
    }
}