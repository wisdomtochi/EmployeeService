using EmployeeService.Enums;

namespace EmployeeService.DTO.Write
{
    public class EmployeeDTOw
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public int Salary { get; set; }
    }
}
