using EmployeeService.Domains;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.DTO.Read
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 3,
                    FirstName = "Misyn",
                    LastName = "Kisera",
                    Gender = GenderEnum.Female,
                    Salary = 847300
                },
                new Employee
                {
                    Id = 4,
                    FirstName = "Jurome",
                    LastName = "Anthony",
                    Gender = GenderEnum.Male,
                    Salary = 324300
                },
                new Employee
                {
                    Id = 5,
                    FirstName = "Doseel",
                    LastName = "Paul",
                    Gender = GenderEnum.Male,
                    Salary = 332300
                }
                );
        }
    }
}
