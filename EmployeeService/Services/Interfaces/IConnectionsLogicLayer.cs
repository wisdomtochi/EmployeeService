using EmployeeService.Domains;

namespace EmployeeService.Services.Interfaces
{
    public interface IConnectionsLogicLayer
    {
        Task<string> AddToConnection(Employee employee);
        //Employee SeeConnectionsRequest(Employee employee);
    }
}
