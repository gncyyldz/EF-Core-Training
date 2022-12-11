using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;

ApplicationDbContext context = new();

#region Connection Resiliency Nedir?
//EF Core üzerinde yapılan veritabanı çalışmaları sürecinde ister istemez veritabanı bağlantısında kopuşlar/kesintiler vs. meydana gelebilmektedir. 

//Connection Resiliency ile kopan bağlantıyı tekrar kurmak için gerekli tekrar bağlantı taleplerinde bulunabilir ve biryandan da execution strategy dediğimiz davranış modellerini belirleyerek bağlantıların kopması durumunda tekrar edecek olan sorguları baştan sona yeniden tetikleyebiliriz.
#endregion
#region EnableRetryOnFailure
//Uygulama sürecinde veritabanı bağlantısı koptuğu taktirde bu yapılandırma sayesinde bağlantıyı tekrardan kurmaya çalışabiliyirouz.

//while (true)
//{
//    await Task.Delay(2000);
//    var persons = await context.Persons.ToListAsync();
//    persons.ForEach(p => Console.WriteLine(p.Name));
//    Console.WriteLine("*******************");
//}

#region MaxRetryCount
//Yeniden bağlantı sağlanması durumunun kaç kere gerçekleştirlecğeini bildirmektedir.
//Defualt değeri 6'dır.
#endregion
#region MaxRetryDelay
//Yeniden bağlantı sağlanması periyodunu bildirmektedir.
//Default değeri 30'dur.
#endregion
#endregion

#region Execution Strategies
//EF Core ile yapılan bir işlem sürecinde veritabanı bağlatısı koptuğu taktirde yeniden bağlantı denenirken yapılan davranışa/alınan aksiyona Execution Strategy denmektedir.

//Bu stratejiyi default dğerlerde kullanabieceğimiz gibi custom olarak da kendimize göre özelleştireibilir ve bağlantı koptuğu durumlarda istediğimiz aksiyonları alabiliriz.

#region Default Execution Strategy
//Eğer ki Connection Resiliency için EnableRetryOnFailure metodunu kullanıyorsak bu default execution stratgy karşılık gelecektir.
//MaxRetryCoun : 6
//Delay : 30
//Default değerlerin kullanılailmesi için EnableRetryOnFailure metodunun parametresis overload'ının kullanılması gerekmektedir.
#endregion
#region Custom Execution Strategy

#region Oluşturma

#endregion
#region Kullanma - ExecutionStrategy

//while (true)
//{
//    await Task.Delay(2000);
//    var persons = await context.Persons.ToListAsync();
//    persons.ForEach(p => Console.WriteLine(p.Name));
//    Console.WriteLine("*******************");
//}
#endregion

#endregion
#region Bağlantı Koptuğu Anda Execute Edilmesi Gereken Tüm Çalışmaları Tekrar İşlemek
//EF Core ile yapılan çalışma sürecinde veritabanı bağlantısının kesildiği durumlarda, bazen bağlantının tekrardan kurulması tek başına yetmemekte, keszintinin olduğu çalışmanın da baştan tekrardan işlenmesi gerekebilmetkedir. İşte bu tarz durumlara karşılık EF Core Execute - ExecuteAsync fonksiyonunu bizlere sunmaktadır.

//Execute fonksiyonu, içerisine vermiş olduğumuz kodları commit edilene kadar işleyecektir. Eğer ki bağlantı kesilmesi meydana gelirse, bağlantının tekrardan kurulması durumunda Execute içerisindeki çalışmalar tekrar baştan işlenecek ve böylece yapılan işlemin tutarlılığı için gerekli çalışma sağlanmış olacaktır.

//var strategy = context.Database.CreateExecutionStrategy();
//await strategy.ExecuteAsync(async () =>
//{
//    using var transcation = await context.Database.BeginTransactionAsync();
//    await context.Persons.AddAsync(new() { Name = "Hilmi" });
//    await context.SaveChangesAsync();

//    await context.Persons.AddAsync(new Person() { Name = "Şuayip" });
//    await context.SaveChangesAsync();

//    await transcation.CommitAsync();
//});

#endregion
#region Execution Strategy Hangi Durumlarda Kullanılır?
//Veritabanının şifresi belirli periyotlarda otomatik olarak değişen uygulamalarda güncel şifreyle connection string'i sağlayacak bir operasyonu custom execution strategy belirleyerek gerçekleştitrebilirsiniz.
#endregion
#endregion

public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        #region Default Execution Strategy
        //optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDB;User ID=SA;Password=1q2w3e4r+!;TrustServerCertificate=True", builder => builder.EnableRetryOnFailure(
        //    maxRetryCount: 5,
        //    maxRetryDelay: TimeSpan.FromSeconds(15),
        //    errorNumbersToAdd: new[] { 4060 }))
        //    .LogTo(
        //    filter: (eventId, level) => eventId.Id == CoreEventId.ExecutionStrategyRetrying,
        //    logger: eventData =>
        //    {
        //        Console.WriteLine($"Bağlantı tekrar kurulmaktadır.");
        //    });
        #endregion
        #region Custom Execution Strategy
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDB;User ID=SA;Password=1q2w3e4r+!;TrustServerCertificate=True", builder => builder.ExecutionStrategy(dependencies => new CustomExecutionStrategy(dependencies, 10, TimeSpan.FromSeconds(15))));
        #endregion
    }
}

class CustomExecutionStrategy : ExecutionStrategy
{
    public CustomExecutionStrategy(ExecutionStrategyDependencies dependencies, int maxRetryCount, TimeSpan maxRetryDelay) : base(dependencies, maxRetryCount, maxRetryDelay)
    {
    }

    public CustomExecutionStrategy(DbContext context, int maxRetryCount, TimeSpan maxRetryDelay) : base(context, maxRetryCount, maxRetryDelay)
    {
    }

    int retryCount = 0;
    protected override bool ShouldRetryOn(Exception exception)
    {
        //Yeniden bağlantı durumunun söz konusu olduğu anlarda yapılacak işlemler...
        Console.WriteLine($"#{++retryCount}. Bağlantı tekrar kuruluyor...");
        return true;
    }
}