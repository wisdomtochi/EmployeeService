using EmployeeService.Helpers;

namespace EmployeeService.Services.Interfaces
{
    public interface IConnectionRequestService
    {
        //Task<IEnumerable<Employee>> GetConnectionRequestList(int Id);
        Task<Result> SendConnectionRequest(Guid receiverId, Guid senderId);
        //Task<string> RemoveConnectionRequest(int employeeId, int requestId);
    }
}
