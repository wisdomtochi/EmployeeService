using EmployeeService.Domains;

namespace EmployeeService.Services.Interfaces
{
    public interface IConnectionsLogicLayer
    {
        Task<string> AddToConnection(Employee employeeModel);
        //IEnumerable<Employee> ConnectionList();
        //IEnumerable<Employee> ConnectionsRequestList();
        //void SendConnectionRequest(Employee emp, ConnectEmployeeViewModel connectEmployee);
    }
}
