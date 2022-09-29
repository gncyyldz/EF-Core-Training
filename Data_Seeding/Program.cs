using Microsoft.EntityFrameworkCore;

ApplicationDbContext context = new();

//Seed Data'lar migration'ların dışında eklenmesi ve değiştirilmesi beklenmeyen durumlar için kullanılan bir özelliktir!

#region Data Seeding Nedir?
//EF Core ile inşa edilen veritabanı içerisinde veritabanı nesneleri olabileceği gibi verilerinde migrate sürecinde üretilmesini isteyebiliriz.
//İşte bu ihtiyaca istinaden Seed Data özelliği ile EF Core üzerinden migrationlarda veriler oluşturabilir ve migrate ederken bu verileri hedef tablolarımıza basabiliriz.
//Seed Data'lar, migrate süreçlerinde hazır verileri tablolara basabilmek için bunları yazılım kısmında tutmamızı gerektirmektedirler. Böylece bu veriler üzerinde veritabanı seviyesainde istenilen manipülasyonlar gönül rahatlığıyla gerçekleştirilebilmektedir.

//Data Seeding özelliği şu noktalarda kullanılabilir;
//Test için geçici verilere ihtiyaç varsa,
//Asp.NET Core'daki Identity yapılanmasındaki roller gibi static değerler de tutulabilir.
//Yazılım için temel konfigürasyonel değerler.
#endregion
#region Seed Data Ekleme
//OnModelCreating metodu içerisinde Entity fonksiyonundan sonra çağrıulan HasData fonksiyonu ilgili entitye karşılık Seed Data'ları eklememizi sağlayan bir fonksiyondur.

//PK değerlerinin manuel olarak bildirilmesi/verilmesi gerekmektedir. Neden diye sorarsanız eğer, ilişkisel verileri de Seed Datalarla üretebilmek için...
#endregion
#region İlişkisel Tablolar İçin Seed Data Ekleme
//İlişkisel senaryolarda dependent table'a veri eklerken foreign key kolonunun propertysi varsa eğer ona ilişkisel değerini vererek ekleme işlemini yapıyoruz.
#endregion
#region Seed Datanın Primary Key'ini Değiştirme
//Eğer ki migrate edilen herhangi bir seed datanın sonrasında PK'i değiştirilirse bu datayla varsa ilişkisel başka veriler onlara cascade davranışı sergilenecektir.
#endregion



class Post
{
    public int Id { get; set; }
    public int BlogId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public Blog Blog { get; set; }
}
class Blog
{
    public int Id { get; set; }
    public string Url { get; set; }

    public ICollection<Post> Posts { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Blog> Blogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Post>()
            .HasData(
                new Post() { Id = 1, BlogId = 1, Title = "A", Content = "..." },
                new Post() { Id = 2, BlogId = 1, Title = "B", Content = "..." },
                new Post() { Id = 5, BlogId = 2, Title = "B", Content = "..." }
            );

        modelBuilder.Entity<Blog>()
            .HasData(
                new Blog() { Id = 11, Url = "www.gencayyildiz.com/blog" },
                new Blog() { Id = 2, Url = "www.bilmemne.com/blog" }
            );
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User ID=SA;Password=1q2w3e4r+!");
    }
}