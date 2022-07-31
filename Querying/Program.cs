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
#region Method Syntax

#endregion
#region Query Syntax

#endregion
#endregion

#region Where
#region Method Syntax

#endregion
#region Query Syntax

#endregion
#endregion

#region OrderBy
#region Method Syntax

#endregion
#region Query Syntax

#endregion
#endregion

#region ThenBy

#endregion

#region OrderByDescending
#region Method Syntax

#endregion
#region Query Syntax

#endregion
#endregion

#region ThenByDescending

#endregion
#endregion

#region Tekil Veri Getiren Sorgulama Fonksiyonları
#region SingleAsync
#region Tek Kayıt Geldiğinde

#endregion
#region Hiç Kayıt Gelmediğinde

#endregion
#region Çok Kayıt Geldiğinde

#endregion
#endregion

#region SingleOrDefaultAsync
#region Tek Kayıt Geldiğinde

#endregion
#region Hiç Kayıt Gelmediğinde

#endregion
#region Çok Kayıt Geldiğinde

#endregion
#endregion

#region FirstAsync
#region Tek Kayıt Geldiğinde

#endregion
#region Hiç Kayıt Gelmediğinde

#endregion
#region Çok Kayıt Geldiğinde

#endregion
#endregion

#region FirstOrDefaultAsync
#region Tek Kayıt Geldiğinde

#endregion
#region Hiç Kayıt Gelmediğinde

#endregion
#region Çok Kayıt Geldiğinde

#endregion
#endregion

#region LastAsync

#endregion

#region LastOrDefaultAsync

#endregion

#region SingleAsync, SingleOrDefaultAsync, FirstAsync, FirstOrDefaultAsync Karşılaştırması

#endregion

#region FindAsync
#region Composite Primary key Durumu

#endregion
#endregion

#region FindAsync İle SingleAsync, SingleOrDefaultAsync, FirstAsync, FirstOrDefaultAsync Fonksiyonlarının Karşılaştırması

#endregion
#endregion

#region Diğer Sorgulama Fonksiyonları
#region CountAsync

#endregion

#region LongCountAsync

#endregion

#region AnyAsync

#endregion

#region MaxAsync

#endregion

#region MinAsync

#endregion

#region Distinct

#endregion

#region AllAsync

#endregion

#region SumAsync

#endregion

#region AverageAsync

#endregion

#region ContainsAsync

#endregion
#endregion

#region Sorgu Sonucu Dönüşüm Fonksiyonları
#region ToDictionaryAsync

#endregion

#region ToArrayAsync

#endregion

#region Select

#endregion

#region SelectMany

#endregion
#endregion

#region GroupBy Fonksiyonu

#endregion

#region Foreach Fonksiyonu

#endregion

public class ETicaretContext : DbContext
{
    public DbSet<Urun> Urunler { get; set; }
    public DbSet<Parca> Parcalar { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ETicaretDB;User ID=SA;Password=1q2w3e4r+!");
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