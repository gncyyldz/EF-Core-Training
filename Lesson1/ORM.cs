using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Lesson1
{
    public class NorthwindDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ConfigurationManager configuration = new();
            configuration.AddJsonFile("appsettings.json");
            optionsBuilder.UseSqlServer($"Server=localhost, 1433;Database=Northwind;User Id=sa;Password={configuration["Password"]}");
        }
    }
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string? Title { get; set; }
        public string? TitleOfCourtesy { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? HomePhone { get; set; }
        public string? Extension { get; set; }
        public string? Notes { get; set; }
        public int? ReportsTo { get; set; }
        public string? PhotoPath { get; set; }
        public string? Region { get; set; }
    }
}
