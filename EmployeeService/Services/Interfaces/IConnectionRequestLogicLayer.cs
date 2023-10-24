using EmployeeService.Domains;

namespace EmployeeService.Services.Interfaces
{
    public interface IConnectionRequestLogicLayer
    {
        Task<IEnumerable<Employee>> GetConnectionRequestList(int id);
        Task SendConnectionRequest(int employeeId, int requestId);
    }
}
