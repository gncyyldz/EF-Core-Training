using Microsoft.EntityFrameworkCore;
Console.WriteLine();

#region Veri Nasıl Silinir?
//ETicaretContext context = new();
//Urun urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 5);
//context.Urunler.Remove(urun);
//await context.SaveChangesAsync();
#endregion
#region Silme İşleminde ChangeTracker'ın Rolü
//ChangeTracker, context üzerinden gelen verilerin takibinden sorumlu bir mekanizmadır. Bu takip mekanizması sayesinde context üzerinden gelen verilerle ilgili işlemler neticesinde update yahut delete sorgularının oluşturulacağı anlaşılır!
#endregion
#region Takip Edilmeyen Nesneler Nasıl Silinir?
//ETicaretContext context = new();
//Urun u = new()
//{
//    Id = 2
//};
//context.Urunler.Remove(u);
//await context.SaveChangesAsync();

#region EntityState İle Silme İşlemi
//Urun u = new() { Id = 1 };
//context.Entry(u).State = EntityState.Deleted;
//await context.SaveChangesAsync();
#endregion
#endregion
#region Birden Fazla Veri Silinirken Nelere Dikkat Edilmelidir?
#region SaveChanges'ı Verimli Kullanalım

#endregion
#region RemoveRange
//ETicaretContext context = new();
//List<Urun> urunler = await context.Urunler.Where(u => u.Id >= 7 && u.Id <= 9).ToListAsync();
//context.Urunler.RemoveRange(urunler);
//await context.SaveChangesAsync();
#endregion
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