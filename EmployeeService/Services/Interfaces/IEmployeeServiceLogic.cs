using EmployeeService.Domains;
using EmployeeService.DTO.Write;
using EmployeeService.Helpers;

namespace EmployeeService.Services.Interfaces
{
    public interface IEmployeeServiceLogic
    {
        Task<Result<Employee>> GetEmployee(Guid id);
        Task<Result<List<Employee>>> GetAllEmployee();
        Task<Result<Employee>> CreateEmployee(EmployeeDTOw employee);
        Task<Result> UpdateEmployee(Guid id, EmployeeDTOw employeeModel);
        Task<Result> DeleteEmployee(Guid employeeId);

    }
}
