using EmployeeService.Domains;

namespace EmployeeService.DTO
{
    public class ConnectEmployeeViewModel
    {
        public int Id { get; set; }
        public List<Employee>? Connections { get; set; }
        //public List<Employee>? ConnectionRequest { get; set; }
    }
}
