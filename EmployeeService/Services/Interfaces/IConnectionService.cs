using EmployeeService.DTO.Read;
using EmployeeService.Helpers;

namespace EmployeeService.Services.Interfaces
{
    public interface IConnectionService
    {
        Task<Result<string>> AddToConnection(Guid employeeId, Guid friendId);
        Task<Result<string>> RemoveFromConnection(Guid employeeId, Guid friendId);
        Task<Result<List<ConnectionDTO>>> ConnectionList(Guid employeeId);
    }
}
