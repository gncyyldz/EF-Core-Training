using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Value_Conversions.Configurations;

public class PersonData : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasData(
                 new() { Id = 1, Name = "Ayşe", Gender = "F", Gender2 = Gender.Famele, Married = true },
                 new() { Id = 2, Name = "Hilmi", Gender = "M", Gender2 = Gender.Male, Married = false },
                 new() { Id = 3, Name = "Raziye", Gender = "F", Gender2 = Gender.Famele, Married = true },
                 new() { Id = 4, Name = "Süleyman", Gender = "M", Gender2 = Gender.Male, Married = false },
                 new() { Id = 5, Name = "Fadime", Gender = "F", Gender2 = Gender.Famele, Married = true },
                 new() { Id = 6, Name = "Şuayip", Gender = "M", Gender2 = Gender.Male, Married = true },
                 new() { Id = 7, Name = "Lale", Gender = "F", Gender2 = Gender.Famele, Married = false },
                 new() { Id = 8, Name = "Jale", Gender = "F", Gender2 = Gender.Famele, Married = true },
                 new() { Id = 9, Name = "Rıfkı", Gender = "M", Gender2 = Gender.Male, Married = true },
                 new() { Id = 10, Name = "Muaviye", Gender = "M", Gender2 = Gender.Male, Married = true }
            );
    }
}