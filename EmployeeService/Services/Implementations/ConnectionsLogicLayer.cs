using EmployeeService.Data;
using EmployeeService.Domains;
using EmployeeService.DTO;
using EmployeeService.Services.Interfaces;

namespace EmployeeService.Services.Implementations
{
    public class ConnectionsLogicLayer : IConnectionsLogicLayer
    {
        private readonly EmployeeDbContext context;

        public ConnectionsLogicLayer(EmployeeDbContext context)
        {
            this.context = context;
        }

        public async Task<string> AddToConnection(Employee employeeModel, ConnectEmployeeViewModel connectEmployee)
        {
            var emp = await context.Employees.FindAsync(employeeModel.Id);

            List<Employee> empList = new List<Employee>();



            if (emp != null)
            {

                empList.Add(emp);
                //Connections = connectEmployee.Connections.Add(emp);
            };

            //connectEmployee.Connections.Add(emp);

            return "You have added a new connection";
        }

            return $"{employeeModel.FirstName} {employeeModel.LastName} could not not be found in the database";
        }

    public IEnumerable<Employee> ConnectionList()
    {
        ConnectEmployeeViewModel connectEmployee = new();
        return connectEmployee.Connections;
    }

    public IEnumerable<Employee> ConnectionsRequestList(ConnectEmployeeViewModel connectEmployee)
    {
        return connectEmployee.ConnectionRequest;
    }

    public void SendConnectionRequest(Employee emp, ConnectEmployeeViewModel connectEmployee)
    {
        //Employee emp = new();


        connectEmployee.ConnectionRequest.Add(emp);
    }
}
}
