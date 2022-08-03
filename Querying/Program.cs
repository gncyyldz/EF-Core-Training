using Microsoft.EntityFrameworkCore;


ETicaretContext context = new();

#region En Temel Basit Bir Sorgulama Nasıl Yapılır?
#region Method Syntax
//var urunler = await context.Urunler.ToListAsync();
#endregion
#region Query Syntax
//var urunler2 = await (from urun in context.Urunler
//                      select urun).ToListAsync();
#endregion
#endregion

#region Sorguyu Execute Etmek İçin Ne Yapmamız Gerekmektedir?
#region ToListAsync 
#region Method Syntax
//var urunler = await context.Urunler.ToListAsync();
#endregion
#region Query Syntax
//var urunler = await (from urun in context.Urunler
//select urun).ToListAsync();
#endregion
#endregion

//int urunId = 5;
//string urunAdi = "2";

//var urunler = from urun in context.Urunler
//              where urun.Id > urunId && urun.UrunAdi.Contains(urunAdi)
//              select urun;

//urunId = 200;
//urunAdi = "4";

//foreach (Urun urun in urunler)
//{
//    Console.WriteLine(urun.UrunAdi);
//}

//await urunler.ToListAsync();

#region Foreach

//foreach (Urun urun in urunler)
//{
//    Console.WriteLine(urun.UrunAdi);
//}

#region Deferred Execution(Ertelenmiş Çalışma)
//IQueryable çalışmalarında ilgili kod yazıldığı noktada tetiklenmez/çalıştırılmaz yani ilgili kod yazıldığı noktada sorguyu generate etmez! Nerede eder? Çalıştırıldığı/execute edildiği noktada tetiklenir! İşte bu durumada ertelenmiş çalışma denir!
#endregion
#endregion
#endregion

#region IQueryable ve IEnumerable Nedir? Basit Olarak!

//var urunler = await (from urun in context.Urunler
//                     select urun).ToListAsync();

#region IQueryable
//Sorguya karşılık gelir.
//Ef core üzerinden yapılmış olan sorgunun execute edilmemiş halini ifade eder.
#endregion
#region IEnumerable
//Sorgunun çalıştırılıp/execute edilip verilerin in memorye yüklenmiş halini ifade eder.
#endregion
#endregion

#region Çoğul Veri Getiren Sorgulama Fonksiyonları
#region ToListAsync
//Üretilen sorguyu execute ettirmemizi sağlayan fonksiyondur.

#region Method Syntax
//var urunler = context.Urunler.ToListAsync();
#endregion
#region Query Syntax
//var urunler = (from urun in context.Urunler
//              select urun).ToListAsync();
//var urunler = from urun in context.Urunler
//              select urun;
//var datas = await urunler.ToListAsync();
#endregion
#endregion

#region Where
//Oluşturulan sorguya where şartı eklememizi sağlayan bir fonksiyondur.

#region Method Syntax
//var urunler = await context.Urunler.Where(u => u.Id > 500).ToListAsync();
//var urunler = await context.Urunler.Where(u => u.UrunAdi.StartsWith("a")).ToListAsync();
//Console.WriteLine();
#endregion
#region Query Syntax
//var urunler = from urun in context.Urunler
//              where urun.Id > 500 && urun.UrunAdi.EndsWith("7")
//              select urun;
//var data = await urunler.ToListAsync();
//Console.WriteLine();
#endregion
#endregion

#region OrderBy
//Sorgu üzerinde sıralama yapmamızı sağlayan bir fonksiyondur. (Ascending)

#region Method Syntax
//var urunler = context.Urunler.Where(u => u.Id > 500 || u.UrunAdi.EndsWith("2")).OrderBy(u => u.UrunAdi);
#endregion
#region Query Syntax
//var urunler2 = from urun in context.Urunler
//               where urun.Id > 500 || urun.UrunAdi.StartsWith("2")
//               orderby urun.UrunAdi
//               select urun;
#endregion

//await urunler.ToListAsync();
//await urunler2.ToListAsync();
#endregion

#region ThenBy
//OrderBy üzerinde yapılan sıralama işlemini farklı kolonlarada uygulamamızı sağlayan bir fonksiyondur. (Ascending) 

//var urunler = context.Urunler.Where(u => u.Id > 500 || u.UrunAdi.EndsWith("2")).OrderBy(u => u.UrunAdi).ThenBy(u => u.Fiyat).ThenBy(u => u.Id);

//await urunler.ToListAsync();
#endregion

#region OrderByDescending
//Descending olarak sıralama yapmamızı sağlayan bir fonksiyondur.

#region Method Syntax
//var urunler = await context.Urunler.OrderByDescending(u => u.Fiyat).ToListAsync();
#endregion
#region Query Syntax
//var urunler = await (from urun in context.Urunler
//                     orderby urun.UrunAdi descending
//                     select urun).ToListAsync();
#endregion
#endregion

#region ThenByDescending
//OrderByDescending üzerinde yapılan sıralama işlemini farklı kolonlarada uygulamamızı sağlayan bir fonksiyondur. (Ascending)
//var urunler = await context.Urunler.OrderByDescending(u => u.Id).ThenByDescending(u => u.Fiyat).ThenBy(u => u.UrunAdi).ToListAsync();
#endregion
#endregion

#region Tekil Veri Getiren Sorgulama Fonksiyonları
//Yapılan sorguda sade ve sadece tek bir verinin gelmesi amaçlanıyorsa Single ya da SingleOrDefault fonksiyonları kullanılabilir.
#region SingleAsync
//Eğer ki, sorgu neticesinde birden fazla veri geliyorsa ya da hiç gelmiyorsa her iki durumda da exception fırlatır.
#region Tek Kayıt Geldiğinde
//var urun = await context.Urunler.SingleAsync(u => u.Id == 55);
#endregion
#region Hiç Kayıt Gelmediğinde
//var urun = await context.Urunler.SingleAsync(u => u.Id == 5555);
#endregion
#region Çok Kayıt Geldiğinde
//var urun = await context.Urunler.SingleAsync(u => u.Id > 55);
#endregion
#endregion

#region SingleOrDefaultAsync
//Eğer ki, sorgu neticesinde birden fazla veri geliyorsa exception fırlatır, hiç veri gelmiyorsa null döner.
#region Tek Kayıt Geldiğinde
//var urun = await context.Urunler.SingleOrDefaultAsync(u => u.Id == 55);
#endregion
#region Hiç Kayıt Gelmediğinde
//var urun = await context.Urunler.SingleOrDefaultAsync(u => u.Id == 5555);
#endregion
#region Çok Kayıt Geldiğinde
//var urun = await context.Urunler.SingleOrDefaultAsync(u => u.Id > 55);
#endregion
#endregion

//Yapılan sorguda tek bir verinin gelmesi amaçlanıyorsa First ya da FirstOrDefault fonksiyonları kullanılabilir.
#region FirstAsync
//Sorgu neticesinde elde edilen verilerden ilkini getirir. Eğer ki hiç veri gelmiyorsa hata fırlatır.
#region Tek Kayıt Geldiğinde
//var urun = await context.Urunler.FirstAsync(u => u.Id == 55);
#endregion
#region Hiç Kayıt Gelmediğinde
//var urun = await context.Urunler.FirstAsync(u => u.Id == 5555);
#endregion
#region Çok Kayıt Geldiğinde
//var urun = await context.Urunler.FirstAsync(u => u.Id > 55);
#endregion
#endregion

#region FirstOrDefaultAsync
//Sorgu neticesinde elde edilen verilerden ilkini getirir. Eğer ki hiç veri gelmiyorsa null değerini döndürür.
#region Tek Kayıt Geldiğinde
//var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 55);
#endregion
#region Hiç Kayıt Gelmediğinde
//var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 5555);
#endregion
#region Çok Kayıt Geldiğinde
//var urun = await context.Urunler.FirstAsync(u => u.Id > 55);
#endregion
#endregion

#region SingleAsync, SingleOrDefaultAsync, FirstAsync, FirstOrDefaultAsync Karşılaştırması

#endregion

#region FindAsync
//Find fonksiyonu, primary key kolonuna özel hızlı bir şekilde sorgulama yapmamızı sağlayan bir fonksiyondur.
//Urun urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 55);
//Urun urun = await context.Urunler.FindAsync(55);

#region Composite Primary key Durumu
//UrunParca u = await context.UrunParca.FindAsync(2, 5);
#endregion
#endregion

#region FindAsync İle SingleAsync, SingleOrDefaultAsync, FirstAsync, FirstOrDefaultAsync Fonksiyonlarının Karşılaştırması

#endregion

#region LastAsync
//Sorgu neticesinde gelen verilerden en sonuncusunu getirir. Eğer ki hiç veri gelmiyorsa hata fırlatır. OrderBy kullanılması mecburidir.
//var urun = await context.Urunler.OrderBy(u => u.Fiyat).LastAsync(u => u.Id > 55);
#endregion

#region LastOrDefaultAsync
//Sorgu neticesinde gelen verilerden en sonuncusunu getirir. Eğer ki hiç veri gelmiyorsa null döner. OrderBy kullanılması mecburidir.
//var urun = await context.Urunler.OrderBy(u => u.Fiyat).LastOrDefaultAsync(u => u.Id > 55);
#endregion
#endregion

#region Diğer Sorgulama Fonksiyonları
#region CountAsync
//Oluşturulan sorgunun execute edilmesi neticesinde kaç adet satırın elde edileceğini sayısal olarak(int) bizlere bildiren fonksiyondur.
//var urunler = (await context.Urunler.ToListAsync()).Count();
//var urunler = await context.Urunler.CountAsync();
#endregion

#region LongCountAsync
//Oluşturulan sorgunun execute edilmesi neticesinde kaç adet satırın elde edileceğini sayısal olarak(long) bizlere bildiren fonksiyondur.
//var urunler = await context.Urunler.LongCountAsync(u => u.Fiyat > 5000);
#endregion

#region AnyAsync
//Sorgu neticesinde verinin gelip gelmediğini bool türünde dönen fonksiyondur. 
//var urunler = await context.Urunler.Where(u => u.UrunAdi.Contains("1")).AnyAsync();
//var urunler = await context.Urunler.AnyAsync(u => u.UrunAdi.Contains("1"));
#endregion

#region MaxAsync
//Verilen kolondaki max değeri getirir.
//var fiyat = await context.Urunler.MaxAsync(u => u.Fiyat);
#endregion

#region MinAsync
//Verilen kolondaki min değeri getirir.
//var fiyat = await context.Urunler.MinAsync(u => u.Fiyat);
#endregion

#region Distinct
//Sorguda mükerrer kayıtlar varsa bunları tekilleştiren bir işleve sahip fonksiyondur.
//var urunler = await context.Urunler.Distinct().ToListAsync();
#endregion

#region AllAsync
//Bir sorgu neticesinde gelen verilerin, verilen şarta uyup uymadığını kontrol etmektedir. Eğer ki tüm veriler şarta uyuyorsa true, uymuyorsa false döndürecektir.
//var m = await context.Urunler.AllAsync(u => u.Fiyat < 15000);
//var m = await context.Urunler.AllAsync(u => u.UrunAdi.Contains("a"));
#endregion

#region SumAsync
//Vermiş olduğumuz sayısal proeprtynin toplamını alır.
//var fiyatToplam = await context.Urunler.SumAsync(u => u.Fiyat);
#endregion

#region AverageAsync
//Vermiş olduğumuz sayısal proeprtynin aritmatik ortalamasını alır.
//var aritmatikOrtalama = await context.Urunler.AverageAsync(u => u.Fiyat);
#endregion

#region Contains
//Like '%...%' sorgusu oluşturmamızı sağlar.
//var urunler = await context.Urunler.Where(u => u.UrunAdi.Contains("7")).ToListAsync();
#endregion

#region StartsWith
//Like '...%' sorgusu oluşturmamızı sağlar.
//var urunler = await context.Urunler.Where(u => u.UrunAdi.StartsWith("7")).ToListAsync();
#endregion

#region EndsWith
//Like '%...' sorgusu oluşturmamızı sağlar.
//var urunler = await context.Urunler.Where(u => u.UrunAdi.EndsWith("7")).ToListAsync();
#endregion
#endregion

#region Sorgu Sonucu Dönüşüm Fonksiyonları
//Bu fonksiyonlar ile sorgu neticesinde elde edilen verileri isteğimiz doğrultuusnda farklı türlerde projecsiyon edebiliyoruz.

#region ToDictionaryAsync
//Sorgu neticesinde gelecek olan veriyi bir dictioanry olarak elde etmek/tutmak/karşılamak istiyorsak eğer kullanılır!
//var urunler = await context.Urunler.ToDictionaryAsync(u => u.UrunAdi, u => u.Fiyat);

//ToList ile aynı amaca hizmet etmektedir. Yani, oluşturulan sorguyu execute edip neticesini alırlar.
//ToList : Gelen sorgu neticesini entity türünde bir koleksiyona(List<TEntity>) dönüştürmekteyken,
//ToDictionary ise : Gelen sorgu neticesini Dictionary türünden bir koleksiyona dönüştürecektir.
#endregion

#region ToArrayAsync
//Oluşturulan sorguyu dizi olarak elde eder.
//ToList ile muadil amaca hizmet eder. Yani sorguyu execute eder lakin gelen sonucu entity dizisi  olarak elde eder.
//var urunler = await context.Urunler.ToArrayAsync();
#endregion

#region Select
//Select fonksiyonunun işlevsel olarak birden fazla davranışı söz konusudur,
//1. Select fonksiyonu, generate edilecek sorgunun çekilecek kolonlarını ayarlamamızı sağlamaktadır. 

//var urunler = await context.Urunler.Select(u => new Urun
//{
//    Id = u.Id,
//    Fiyat = u.Fiyat
//}).ToListAsync();

//2. Select fonksiyonu, gelen verileri farklı türlerde karşılamamızı sağlar. T, anonim

//var urunler = await context.Urunler.Select(u => new 
//{
//    Id = u.Id,
//    Fiyat = u.Fiyat
//}).ToListAsync();


//var urunler = await context.Urunler.Select(u => new UrunDetay
//{
//    Id = u.Id,
//    Fiyat = u.Fiyat
//}).ToListAsync();

#endregion

#region SelectMany
//Select ile aynı amaca hizmet eder. Lakin, ilişkisel tablolar neticesinde gelen koleksiyonel verileri de tekilleştirip projeksiyon etmemizi sağlar.

//var urunler = await context.Urunler.Include(u => u.Parcalar).SelectMany(u => u.Parcalar, (u, p) => new
//{
//    u.Id,
//    u.Fiyat,
//    p.ParcaAdi
//}).ToListAsync();
#endregion
#endregion

#region GroupBy Fonksiyonu
//Gruplama yapmamızı sağlayan fonksiyondur.
#region Method Syntax
//var datas = await context.Urunler.GroupBy(u => u.Fiyat).Select(group => new
//{
//    Count = group.Count(),
//    Fiyat = group.Key
//}).ToListAsync();
#endregion
#region Query Syntax
var datas = await (from urun in context.Urunler
                   group urun by urun.Fiyat
            into @group
                   select new
                   {
                       Fiyat = @group.Key,
                       Count = @group.Count()
                   }).ToListAsync();
#endregion
#endregion

#region Foreach Fonksiyonu
//Bir sorgulama fonksiyonu felan değildir!
//Sorgulama neticesinde elde edilen koleksiyonel veriler üzerinde iterasyonel olarak dönmemizi ve teker teker verileri elde edip işlemler yapabilmemizi sağlayan bir fonksiyondur. foreach döngüsünün metot halidir!

foreach (var item in datas)
{

}
datas.ForEach(x =>
{
    
});
#endregion


public class ETicaretContext : DbContext
{
    public DbSet<Urun> Urunler { get; set; }
    public DbSet<Parca> Parcalar { get; set; }
    public DbSet<UrunParca> UrunParca { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ETicaretDB;User ID=SA;Password=1q2w3e4r+!");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UrunParca>().HasKey(up => new { up.UrunId, up.ParcaId });
    }
}
public class Urun
{
    public int Id { get; set; }
    public string UrunAdi { get; set; }
    public float Fiyat { get; set; }

    public ICollection<Parca> Parcalar { get; set; }
}
public class Parca
{
    public int Id { get; set; }
    public string ParcaAdi { get; set; }
}
public class UrunParca
{
    public int UrunId { get; set; }
    public int ParcaId { get; set; }

    public Urun Urun { get; set; }
    public Parca Parca { get; set; }
}

public class UrunDetay
{
    public int Id { get; set; }
    public float Fiyat { get; set; }
}