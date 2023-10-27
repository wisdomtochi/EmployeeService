using EmployeeService.DTO;
using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Domains
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public GenderEnum Gender { get; set; }
        public int Salary { get; set; }
        public List<Connection> Connections { get; set; } = new();
        public List<Employee> Requests { get; set; } = new();
    }
}
