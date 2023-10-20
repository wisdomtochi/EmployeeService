using EmployeeService.Data;
using EmployeeService.Domains;
using EmployeeService.DTO;
using EmployeeService.Services.Interfaces;

namespace EmployeeService.Services.Implementations
{
    public class ConnectionsLogicLayer : IConnectionsLogicLayer
    {
        private readonly EmployeeDbContext context;

        public ConnectionsLogicLayer(EmployeeDbContext context, ConnectEmployeeViewModel connectEmployee)
        {
            this.context = context;
        }

        public async Task<string> AddToConnection(Employee employeeModel)
        {
            var emp = await context.Employees.FindAsync(employeeModel.Id);

            if (emp == null)
            {
                return $"{employeeModel.FirstName} {employeeModel.LastName} could not not be found in the database";
            };

            return "You have added a new connection";
        }

        //public IEnumerable<Employee> ConnectionList()
        //{

        //    return connectEmployee.Connections;
        //}

        //public IEnumerable<Employee> ConnectionsRequestList()
        //{
        //    return connectEmployee.ConnectionRequest;
        //}

        //public void SendConnectionRequest(Employee emp, ConnectEmployeeViewModel connectEmployee)
        //{
        //    if (connectEmployee.Id == emp.Id)
        //    {
        //        Employee employee = new();
        //        connectEmployee.ConnectionRequest.Add(employee);
        //    }
        //}
    }
}
