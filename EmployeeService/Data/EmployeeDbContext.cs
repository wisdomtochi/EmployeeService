using EmployeeService.Domains;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext() { }
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
            : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Connection> Connections { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    FirstName = "Miselyn",
                    LastName = "Kisera",
                    Gender = "F",
                    Salary = 847300
                },
                new Employee
                {
                    Id = 2,
                    FirstName = "Jurome",
                    LastName = "Anthony",
                    Gender = "M",
                    Salary = 324300
                },
                new Employee
                {
                    Id = 3,
                    FirstName = "Doseel",
                    LastName = "Paul",
                    Gender = "F",
                    Salary = 332300
                }
                );
            builder.Entity<Connection>()
                .HasMany(e => e.EmployeeIds)
                .WithMany(e => e.Id);
        }


    }
}
