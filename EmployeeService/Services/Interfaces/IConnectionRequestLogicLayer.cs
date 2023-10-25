using EmployeeService.Domains;

namespace EmployeeService.Services.Interfaces
{
    public interface IConnectionRequestLogicLayer
    {
        Task<IEnumerable<Employee>> GetConnectionRequestList(int Id);
        Task SendConnectionRequest(int employeeId, int requestId);
    }
}
