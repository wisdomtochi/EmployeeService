using EmployeeService.DTO.Read;
using EmployeeService.Helpers;

namespace EmployeeService.Services.Interfaces
{
    public interface IConnectionRequestService
    {
        Task<Result<List<RequestDTO>>> GetConnectionRequests(Guid Id);
        Task<Result<string>> SendConnectionRequest(Guid receiverId, Guid senderId);
        Task<Result<string>> RemoveConnectionRequest(Guid senderId, Guid receiverId);
    }
}
