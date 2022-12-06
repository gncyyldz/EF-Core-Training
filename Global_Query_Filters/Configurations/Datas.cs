using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Global_Query_Filters.Configurations;

public class PersonData : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasData(new Person[]
        {
              new(){ PersonId  = 1, Name = "Ayşe" , IsActive = true },
              new(){ PersonId  = 2, Name = "Hilmi" , IsActive = false },
              new(){ PersonId  = 3, Name = "Raziye" , IsActive = true },
              new(){ PersonId  = 4, Name = "Süleyman" , IsActive = false },
              new(){ PersonId  = 5, Name = "Fadime" , IsActive = true },
              new(){ PersonId  = 6, Name = "Şuayip" , IsActive = true },
              new(){ PersonId  = 7, Name = "Lale" , IsActive = false },
              new(){ PersonId  = 8, Name = "Jale" , IsActive = true },
              new(){ PersonId  = 9, Name = "Rıfkı" , IsActive = false },
              new(){ PersonId  = 10, Name = "Muaviye" , IsActive = false },
        });
    }
}
public class OrderData : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasData(new Order[]
        {
              new(){ OrderId = 1, PersonId = 1, Description = "...", Price = 10 },
              new(){ OrderId = 2, PersonId = 2, Description = "...", Price = 20 },
              new(){ OrderId = 3, PersonId = 4, Description = "...", Price = 30 },
              new(){ OrderId = 4, PersonId = 5, Description = "...", Price = 40 },
              new(){ OrderId = 5, PersonId = 1, Description = "...", Price = 50 },
              new(){ OrderId = 6, PersonId = 6, Description = "...", Price = 60 },
              new(){ OrderId = 7, PersonId = 7, Description = "...", Price = 70 },
              new(){ OrderId = 8, PersonId = 1, Description = "...", Price = 80 },
              new(){ OrderId = 9, PersonId = 8, Description = "...", Price = 90 },
              new(){ OrderId = 10, PersonId = 9, Description = "...", Price = 100 },
              new(){ OrderId = 11, PersonId = 1, Description = "...", Price = 110 },
              new(){ OrderId = 12, PersonId = 2, Description = "...", Price = 120 },
              new(){ OrderId = 13, PersonId = 2, Description = "...", Price = 130 },
              new(){ OrderId = 14, PersonId = 3, Description = "...", Price = 140 },
              new(){ OrderId = 15, PersonId = 1, Description = "...", Price = 150 },
              new(){ OrderId = 16, PersonId = 4, Description = "...", Price = 160 },
              new(){ OrderId = 17, PersonId = 1, Description = "...", Price = 170 },
              new(){ OrderId = 18, PersonId = 1, Description = "...", Price = 180 },
              new(){ OrderId = 19, PersonId = 5, Description = "...", Price = 190 },
              new(){ OrderId = 20, PersonId = 6, Description = "...", Price = 200 },
              new(){ OrderId = 21, PersonId = 1, Description = "...", Price = 210 },
              new(){ OrderId = 22, PersonId = 7, Description = "...", Price = 220 },
              new(){ OrderId = 23, PersonId = 7, Description = "...", Price = 230 },
              new(){ OrderId = 24, PersonId = 8, Description = "...", Price = 240 },
              new(){ OrderId = 25, PersonId = 1, Description = "...", Price = 250 },
              new(){ OrderId = 26, PersonId = 1, Description = "...", Price = 260 },
              new(){ OrderId = 27, PersonId = 9, Description = "...", Price = 270 },
              new(){ OrderId = 28, PersonId = 9, Description = "...", Price = 280 },
              new(){ OrderId = 29, PersonId = 9, Description = "...", Price = 290 },
              new(){ OrderId = 30, PersonId = 2, Description = "...", Price = 300 },
              new(){ OrderId = 31, PersonId = 3, Description = "...", Price = 310 },
              new(){ OrderId = 32, PersonId = 1, Description = "...", Price = 320 },
              new(){ OrderId = 33, PersonId = 1, Description = "...", Price = 330 },
              new(){ OrderId = 34, PersonId = 1, Description = "...", Price = 340 },
              new(){ OrderId = 35, PersonId = 5, Description = "...", Price = 350 },
              new(){ OrderId = 36, PersonId = 1, Description = "...", Price = 360 },
              new(){ OrderId = 37, PersonId = 5, Description = "...", Price = 370 },
              new(){ OrderId = 38, PersonId = 1, Description = "...", Price = 380 },
              new(){ OrderId = 39, PersonId = 1, Description = "...", Price = 390 },
              new(){ OrderId = 40, PersonId = 1, Description = "...", Price = 400 },
        });
    }
}