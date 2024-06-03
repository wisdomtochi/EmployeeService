using EmployeeService.DTO.Read;
using EmployeeService.DTO.Write;
using EmployeeService.Enums;
using EmployeeService.Helpers;

namespace EmployeeService.Services.Interfaces
{
    public interface IEmployeeServiceLogic
    {
        Task<Result<EmployeeDTO>> GetEmployee(Guid id);
        Task<Result<List<EmployeeDTO>>> GetAllEmployee();
        Task<Result> CreateEmployee(EmployeeDTOw employee);
        Task<Result> UpdateEmployee(Guid id, EmployeeDTOw employeeModel);
        Task<Result> DeleteEmployee(Guid employeeId);
        Task<Result<List<EmployeeDTO>>> Search(string name, Gender? gender);
    }
}
