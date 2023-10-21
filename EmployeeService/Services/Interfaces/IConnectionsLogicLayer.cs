using EmployeeService.Domains;

namespace EmployeeService.Services.Interfaces
{
    public interface IConnectionsLogicLayer
    {
        Task<string> AddToConnection(int employeeId, int connectionId);
        IEnumerable<Connection> ConnectionList();
        //IEnumerable<Employee> ConnectionsRequestList();
        //void SendConnectionRequest(Employee emp, ConnectEmployeeViewModel connectEmployee);
        Task<List<Connection>> GetConnectionList(int Id);
    }
}
