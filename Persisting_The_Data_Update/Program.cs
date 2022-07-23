using Microsoft.EntityFrameworkCore;

#region Veri Nasıl Güncellenir?
//ETicaretContext context = new();

//Urun urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 3);
//urun.UrunAdi = "H Ürünü";
//urun.Fiyat = 999;

//await context.SaveChangesAsync();
#endregion
#region ChangeTracker Nedir? Kısaca!
//ChangeTracker, context üzerinden gelen verilerin takibinden sorumlu bir mekanizmadır. Bu takip mekanizması sayesinde context üzerinden gelen verilerle ilgili işlemler neticesinde update yahut delete sorgularının oluşturulacağı anlaşılır!
#endregion
#region Takip Edilmeyen Nesneler Nasıl Güncellenir?
//ETicaretContext context = new();
//Urun urun = new()
//{
//    Id = 3,
//    UrunAdi = "Yeni Ürün",
//    Fiyat = 123
//};

#region Update Fonksiyonu
//ChangeTracker mekanizması tarafından takip edilmeyen nesnelerin güncellenebilmesi için Update fonksiyonu kullanılır!
//Update fonksiyonunu kullanabilmek için kesinlikle ilgili nesnede Id değeri verilmelidir! Bu değer güncellenecek(update sorgusu oluşturulacak) verinin hangisi olduğunu ifade edecektir.
//context.Urunler.Update(urun);
//await context.SaveChangesAsync();
#endregion
#endregion
#region EntityState Nedir?
//Bir entity instance'ının durumunu ifade eden bir referanstır.
//ETicaretContext context = new();
//Urun u = new();
//Console.WriteLine(context.Entry(u).State);
#endregion
#region EF Core Açısından Bir Verinin Güncellenmesi Gerektiği Nasıl Anlaşılıyor?
//ETicaretContext context = new();
//Urun urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 3);
//Console.WriteLine(context.Entry(urun).State);

//urun.UrunAdi = "Hilmi";

//Console.WriteLine(context.Entry(urun).State);

//await context.SaveChangesAsync();

//Console.WriteLine(context.Entry(urun).State);

//urun.Fiyat = 999;

//Console.WriteLine(context.Entry(urun).State);
#endregion
#region Birden Fazla Veri Güncellenirken Nelere Dikkat Edilmelidir?
ETicaretContext context = new();

var urunler = await context.Urunler.ToListAsync();
foreach (var urun in urunler)
{
    urun.UrunAdi += "*";
}

await context.SaveChangesAsync();
#endregion

public class ETicaretContext : DbContext
{
    public DbSet<Urun> Urunler { get; set; }

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
}