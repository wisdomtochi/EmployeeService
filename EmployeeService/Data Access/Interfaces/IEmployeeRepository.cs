using EmployeeService.Domains;

namespace EmployeeService.Data_Access
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployee(int id);
        Task<IEnumerable<Employee>> GetAllEmployee();
        Task<Employee> CreateEmployee(Employee employee);
        Task UpdateEmployee(Employee employeeModel);
        Task DeleteEmployee(int employeeId);

    }
}
