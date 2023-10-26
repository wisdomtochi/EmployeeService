using EmployeeService.Domains;

namespace EmployeeService.Services.Interfaces
{
    public interface IConnectionRequestLogicLayer
    {
        Task<IEnumerable<Employee>> GetConnectionRequestList(int Id);
        Task SendConnectionRequest(int receiverId, int senderId);
        Task<string> RemoveConnectionRequest(int employeeId, int requestId);
    }
}
