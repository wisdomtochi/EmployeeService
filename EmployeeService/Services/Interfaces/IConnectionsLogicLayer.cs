using EmployeeService.Domains;
using EmployeeService.DTO;

namespace EmployeeService.Services.Interfaces
{
    public interface IConnectionsLogicLayer
    {
        Task<string> AddToConnection(Employee employee);
        IEnumerable<Employee> ConnectionList();
        IEnumerable<Employee> ConnectionsRequestList(ConnectEmployeeViewModel connectEmployee);
        void SendConnectionRequest(ConnectEmployeeViewModel connectEmployee);
    }
}
