using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection;
using System.Text.Json;

ApplicationDbContext context = new();
#region Value Conversions Nedir?
//EF Core üzerinden veritabanı ile yapılan sorgulama süreçlerinde veriler üzerinde dönüşümler yapmamızı sağlayan bir özelliktir.
//SELECT sorguları sürecinde gelecek olan veriler üzerinde dönüşüm yapabiliriz.
//Ya da 
//UPDATE yahut INSERT sorgularında da yazılım züerinden veritabanına gönderdiğimiz veriler üzerinde de dönüşümler yapabilir ve böylece fiziksel olarak da verileri manipüle edebiliriz.
#endregion
#region Value Converter Kullanımı Nasıldır?
//Value conversions özelliğini EF Core'da ki Value COnverter yapıları tarafından uygulayabilmekteyiz.

#region HasConversion
//HasConversion fonksiyonu, en sade haliyle EF Core züerinden value converter özelliği gören bir fonksiyondur.
//var persons = await context.Persons.ToListAsync();
//Console.WriteLine();
#endregion
#endregion
#region Enum Değerler İle Value Converter Kullanımı

//Normal durumlarda Enum türünde tutulan propertylerin veritabanındaki karşılıkları int olacak şekilde aktarımı gerçekleştirlimektedir. Value converter sayesinde enum türünden olan propertylerinde dönüşümlerini istediğimiz türlere sağlayarak hem ilgili kolonun türünü o türde ayarlayaiblir hem de enum üzerinden çalış sürecinde verisel dönüşümleri ilgili türde sağlayabiliriz.

//var person = new Person() { Name = "Rakıf", Gender2 = Gender.Male, Gender = "M" };
//await context.Persons.AddAsync(person);
//await context.SaveChangesAsync();
//var _person = await context.Persons.FindAsync(person.Id);
//Console.WriteLine();

#endregion
#region ValueConverter Sınıfı
//ValueConverter sınıfı, verisel dönüşümlerideki çalışmaları/sorumlulukları üstlenebilecek bir sınıftır.
//Yani bu sınıfın instance'ı ile HasConvention fonksiyonun yapılan çalışmaları üstlenebilir ve direkt bu instance'ı ilgili fonksiyona vererek dönüşümsel çalışmalarımızı gerçekleştirebiliiriz.


//var _person = await context.Persons.FindAsync(13);
//Console.WriteLine();
#endregion
#region Custom ValueConverter Sınıfı
//EF Core'da verisel dönüşümler için custom olarak converter sınıfları üretebilmekteyiz. Bunun için tek yapılması gereken custom sınıfının ValueConverter sınıfından miras almasını sağlamaktadır.
//var _person = await context.Persons.FindAsync(13);
//Console.WriteLine();
#endregion
#region Built-in Converters Yapıları
//EF Core basit dönüşümler için kendi bünyesinde yerleşik convert sınıfları barındırmaktadır.

#region BoolToZeroOneConverter
//bool olan verinin int olarak tutulmasını sağlar.
#endregion
#region BoolToStringConverter
//bool olan verinin string olarak tutulmasını sağlar.
#endregion
#region BoolToTwoValuesConverter
//bool olan verinin char olarak tutulmasını sağlar.
#endregion

//Diğer built-in converters yapılarını aşağıdaki linkten gözlemleyebilirsiniz.
//https://learn.microsoft.com/en-us/ef/core/modeling/value-conversions?tabs=data-annotations#built-in-converters

#endregion
#region İlkel Koleksiyonların Serilizasyonu
//İçerisinde ilkel türlerden olyuşturulmuş koleksiyonları barındıran modelleri migrate etmeye çalıştığımızda hata ile karşılaşmaktayız. By hatadan kurtuılmak ve ilgili veriye koleksiyondaki verileri serilize ederek işleyebilmek için bu koleksiyonu normal metinsel değerlere dönüştürmemize fırsat veren bir conversion operasyonu gerçekleştireibliriz. 

//var person = new Person() { Name = "Filanca", Gender = "M", Gender2 = Gender.Male, Married = true, Titles = new() { "A", "B", "C" } };
//await context.Persons.AddAsync(person);

//await context.SaveChangesAsync();

//var _person = await context.Persons.FindAsync(person.Id);
//Console.WriteLine();
#endregion
#region .NET 6 - Value Converter For Nullable Fields
//.NET 6'dan önce value converter'lar null değerlerin dönüşüşmünü desteklememekteydi. .NET 6 ile artık nul ldeğerler de dönüştürülebilmektedir.
#endregion


public class GenderConverter : ValueConverter<Gender, string>
{
    public GenderConverter() : base(
        //INSERT - UPDATE
        g => g.ToString()
        ,
        //SELECT
        g => (Gender)Enum.Parse(typeof(Gender), g)
        )
    {
    }
}

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Gender { get; set; }
    public Gender Gender2 { get; set; }
    public bool Married { get; set; }
    public List<string>? Titles { get; set; }
}
public enum Gender
{
    Male,
    Famele
}
public class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        #region Value Converter Kullanımı Nasıldır?
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.Gender)
        //    .HasConversion(
        //        //INSERT - UPDATE
        //        g => g.ToUpper()
        //    ,
        //        //SELECT
        //        g => g == "M" ? "Male" : "Female"
        //    );
        #endregion
        #region Enum Değerler İle Value Converter Kullanımı
        //modelBuilder.Entity<Person>()
        //   .Property(p => p.Gender2)
        //   .HasConversion(
        //       //INSERT - UPDATE
        //       g => g.ToString()
        //       //g => (int)g
        //   ,
        //       //SELECT
        //       g => (Gender)Enum.Parse(typeof(Gender), g)
        //   );
        #endregion
        #region ValueConverter Sınıfı

        //ValueConverter<Gender, string> converter = new(
        //     //INSERT - UPDATE
        //     g => g.ToString()
        //     ,
        //     //SELECT
        //     g => (Gender)Enum.Parse(typeof(Gender), g)
        //    );

        //modelBuilder.Entity<Person>()
        // .Property(p => p.Gender2)
        // .HasConversion(converter);
        #endregion
        #region Custom ValueConverter Sınıfı
        //modelBuilder.Entity<Person>()
        // .Property(p => p.Gender2)
        // .HasConversion<GenderConverter>();
        #endregion
        #region BoolToZeroOneConverter
        //modelBuilder.Entity<Person>()
        // .Property(p => p.Married)
        // .HasConversion<BoolToZeroOneConverter<int>>();

        //ya da direkt aşağıdaki gibi int türünü bildirirsek de aynı davranış söz konusu olacaktır.
        //modelBuilder.Entity<Person>()
        // .Property(p => p.Married)
        // .HasConversion<int>();
        #endregion
        #region BoolToStringConverter
        //BoolToStringConverter converter = new("Bekar", "Evli");

        //modelBuilder.Entity<Person>()
        // .Property(p => p.Married)
        // .HasConversion(converter);
        #endregion
        #region BoolToTwoValuesConverter
        //BoolToTwoValuesConverter<char> converter = new('B', 'E');

        //modelBuilder.Entity<Person>()
        // .Property(p => p.Married)
        // .HasConversion(converter);
        #endregion
        #region İlkel Koleksiyonların Serilizasyonu
        modelBuilder.Entity<Person>()
            .Property(p => p.Titles)
            .HasConversion(
            //INSERT - UPDATE
            t => JsonSerializer.Serialize(t, (JsonSerializerOptions)null)
            ,
            //SELECT
            t => JsonSerializer.Deserialize<List<string>>(t, (JsonSerializerOptions)null)
            );
        #endregion
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDB;User ID=SA;Password=1q2w3e4r+!;TrustServerCertificate=True");
    }
}