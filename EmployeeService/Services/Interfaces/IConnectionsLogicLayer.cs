using EmployeeService.Domains;

namespace EmployeeService.Services.Interfaces
{
    public interface IConnectionsLogicLayer
    {
        Task<string> AddToConnection(Employee employeeModel);
        IEnumerable<Connection> ConnectionList();
        //IEnumerable<Employee> ConnectionsRequestList();
        //void SendConnectionRequest(Employee emp, ConnectEmployeeViewModel connectEmployee);
        Task<List<Connection>> GetConnectionList(int Id, Connection connectModel);
    }
}
