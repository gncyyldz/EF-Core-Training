
using Microsoft.EntityFrameworkCore;

ApplicationDbContext context = new();

#region Shadow Properties - Gölge Özellikler
//Entity sınıflarında fiziksel olarak tanımlanmayan/modellenmeyen ancak EF Core tarafından ilgili entity için var olan/var olduğu kabul edilen property'lerdir.
//Tabloda gösterilmesini istemediğimiz/lüzumlu görmediğimiz/entity instance'ı üzerinde işlem yapmayacağımız kolonlar için shadow propertyler kullanılabilir.
//Shadow property'lerin değerleri ve stateleri Change Tracker tarafından kontrol edilir.
#endregion

#region Foreign Key - Shadow Properties
//İlişkisel senaryolarda foreign key property'sini tanımlamadığımız halde EF Core tarafından dependent entity'e eklenmektedir. İşte bu shadow property'dir.

//var blogs = await context.Blogs.Include(b => b.Posts)
//    .ToListAsync();
//Console.WriteLine();
#endregion

#region Shadow Property Oluşturma
//Bir entity üzerinde shadow property oluşturmak istiyorsanız eğer Fluent API'ı kullanmanız gerekmektedir.
//        modelBuilder.Entity<Blog>()
//            .Property<DateTime>("CreatedDate");
#endregion

#region Shadow Property'e Erişim Sağlama
#region ChangeTracker İle Erişim
//Shadow property'e erişim sağlayabilmek için Change Tracker'dan istifade edilebilir.

//var blog = await context.Blogs.FirstAsync();

//var createDate = context.Entry(blog).Property("CreatedDate");
//Console.WriteLine(createDate.CurrentValue);
//Console.WriteLine(createDate.OriginalValue);

//createDate.CurrentValue = DateTime.Now;
//await context.SaveChangesAsync();
#endregion

#region EF.Property İle Erişim
//Özellikle LINQ sorgularında Shadow Propery'lerine erişim için EF.Property static yapılanmasını kullanabiliriz.
//var blogs = await context.Blogs.OrderBy(b => EF.Property<DateTime>(b, "CreatedDate")).ToListAsync();

//var blogs2 = await context.Blogs.Where(b => EF.Property<DateTime>(b, "CreatedDate").Year > 2020).ToListAsync();
//Console.WriteLine();
#endregion
#endregion


class Blog
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Post> Posts { get; set; }
}

class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool lastUpdated { get; set; }

    public Blog Blog { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User ID=SA;Password=1q2w3e4r+!");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>()
            .Property<DateTime>("CreatedDate");

        base.OnModelCreating(modelBuilder);
    }
}