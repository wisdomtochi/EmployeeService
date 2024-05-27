using EmployeeService.Enums;
using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Domains
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderEnum Gender { get; set; }
        public int Salary { get; set; }
    }
}
