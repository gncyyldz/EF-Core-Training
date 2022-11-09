using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Complex_Relational_Queries.Configurations;
public class PhotoData : IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.HasData(new Photo[]
        {
                new(){ PersonId = 1, Url = "https://randomuser.me/api/portraits/women/1.jpg" },
                new(){ PersonId = 2, Url = "https://randomuser.me/api/portraits/men/2.jpg" },
                new(){ PersonId = 3, Url = "https://randomuser.me/api/portraits/women/3.jpg" },
                new(){ PersonId = 4, Url = "https://randomuser.me/api/portraits/men/4.jpg" },
                new(){ PersonId = 5, Url = "https://randomuser.me/api/portraits/women/5.jpg" },
                new(){ PersonId = 6, Url = "https://randomuser.me/api/portraits/men/6.jpg" },
                new(){ PersonId = 7, Url = "https://randomuser.me/api/portraits/women/7.jpg" },
                new(){ PersonId = 8, Url = "https://randomuser.me/api/portraits/women/8.jpg" },
                new(){ PersonId = 9, Url = "https://randomuser.me/api/portraits/men/9.jpg" },
                new(){ PersonId = 10, Url = "https://randomuser.me/api/portraits/women/10.jpg" }
        });
    }
}
public class PersonData : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasData(new Person[]
        {
              new(){ PersonId  = 1, Name = "Ayşe", Gender = Gender.Woman },
              new(){ PersonId  = 2, Name = "Hilmi", Gender = Gender.Man},
              new(){ PersonId  = 3, Name = "Raziye", Gender = Gender.Woman },
              new(){ PersonId  = 4, Name = "Süleyman", Gender = Gender.Man },
              new(){ PersonId  = 5, Name = "Fadime", Gender = Gender.Woman },
              new(){ PersonId  = 6, Name = "Şuayip", Gender = Gender.Man },
              new(){ PersonId  = 7, Name = "Lale", Gender = Gender.Woman },
              new(){ PersonId  = 8, Name = "Jale", Gender = Gender.Woman },
              new(){ PersonId  = 9, Name = "Rıfkı", Gender = Gender.Man },
              new(){ PersonId  = 10, Name = "Muaviye", Gender = Gender.Woman },
        });
    }
}
public class OrderData : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasData(new Order[]
        {
              new(){ OrderId = 1, PersonId = 1, Description = "..."},
              new(){ OrderId = 2, PersonId = 2, Description = "..."},
              new(){ OrderId = 3, PersonId = 4, Description = "..."},
              new(){ OrderId = 4, PersonId = 5, Description = "..."},
              new(){ OrderId = 5, PersonId = 1, Description = "..."},
              new(){ OrderId = 6, PersonId = 6, Description = "..."},
              new(){ OrderId = 7, PersonId = 7, Description = "..."},
              new(){ OrderId = 8, PersonId = 1, Description = "..."},
              new(){ OrderId = 9, PersonId = 8, Description = "..."},
              new(){ OrderId = 10, PersonId = 9, Description = "..."},
              new(){ OrderId = 11, PersonId = 1, Description = "..."},
              new(){ OrderId = 12, PersonId = 2, Description = "..."},
              new(){ OrderId = 13, PersonId = 2, Description = "..."},
              new(){ OrderId = 14, PersonId = 3, Description = "..."},
              new(){ OrderId = 15, PersonId = 1, Description = "..."},
              new(){ OrderId = 16, PersonId = 4, Description = "..."},
              new(){ OrderId = 17, PersonId = 1, Description = "..."},
              new(){ OrderId = 18, PersonId = 1, Description = "..."},
              new(){ OrderId = 19, PersonId = 5, Description = "..."},
              new(){ OrderId = 20, PersonId = 6, Description = "..."},
              new(){ OrderId = 21, PersonId = 1, Description = "..."},
              new(){ OrderId = 22, PersonId = 7, Description = "..."},
              new(){ OrderId = 23, PersonId = 7, Description = "..."},
              new(){ OrderId = 24, PersonId = 8, Description = "..."},
              new(){ OrderId = 25, PersonId = 1, Description = "..."},
              new(){ OrderId = 26, PersonId = 1, Description = "..."},
              new(){ OrderId = 27, PersonId = 9, Description = "..."},
              new(){ OrderId = 28, PersonId = 9, Description = "..."},
              new(){ OrderId = 29, PersonId = 9, Description = "..."},
              new(){ OrderId = 30, PersonId = 2, Description = "..."},
              new(){ OrderId = 31, PersonId = 3, Description = "..."},
              new(){ OrderId = 32, PersonId = 1, Description = "..."},
              new(){ OrderId = 33, PersonId = 1, Description = "..."},
              new(){ OrderId = 34, PersonId = 1, Description = "..."},
              new(){ OrderId = 35, PersonId = 5, Description = "..."},
              new(){ OrderId = 36, PersonId = 1, Description = "..."},
              new(){ OrderId = 37, PersonId = 5, Description = "..."},
              new(){ OrderId = 38, PersonId = 1, Description = "..."},
              new(){ OrderId = 39, PersonId = 1, Description = "..."},
              new(){ OrderId = 40, PersonId = 1, Description = "..."},
        });
    }
}