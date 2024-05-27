using EmployeeService.Domains;
using EmployeeService.Helpers;

namespace EmployeeService.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<Result> GetEmployee(Guid id);
        Task<Result> GetAllEmployee();
        Task<Result> CreateEmployee(Employee employee);
        Task<Result> UpdateEmployee(Guid id, Employee employeeModel);
        Task<Result> DeleteEmployee(Guid employeeId);

    }
}
