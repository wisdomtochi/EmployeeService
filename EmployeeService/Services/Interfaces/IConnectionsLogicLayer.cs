using EmployeeService.Domains;

namespace EmployeeService.Services.Interfaces
{
    public interface IConnectionsLogicLayer
    {
        Task<string> AddToConnection(int employeeId, int connectionId);
        Task<IEnumerable<Connection>> ConnectionList();
        Task<IEnumerable<Employee>> GetEmployeeConnectionList(int Id);
        Task<string> DeleteFromConnection(int employeeId, int connectionId);
    }
}
