using EmployeeService.Helpers;

namespace EmployeeService.Services.Interfaces
{
    public interface IConnectionService
    {
        Task<Result> AddToConnection(Guid employeeId, Guid friendId);
        Task<Result> RemoveFromConnection(Guid employeeId, Guid friendId);
        Task<Result> ConnectionList(Guid employeeId);
        //Task<IEnumerable<Connection>> ConnectionList();
        //Task<IEnumerable<Employee>> GetEmployeeConnectionList(int Id);
        //Task<string> DeleteFromConnection(int employeeId, int connectionId);
    }
}
