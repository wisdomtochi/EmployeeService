using EmployeeService.Data;
using EmployeeService.Domains;
using EmployeeService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace EmployeeService.Services.Implementations
{
    public class ConnectionsLogicLayer : IConnectionsLogicLayer
    {
        private readonly EmployeeDbContext context;

        public ConnectionsLogicLayer(EmployeeDbContext context)
        {
            this.context = context;
        }

        public async Task<string> AddToConnection(Employee employeeModel)
        {
            var emp = await context.Employees.FindAsync(employeeModel.Id);

            //await context.Employees.;

            if (emp == null)
            {
                return $"{employeeModel.FirstName} {employeeModel.LastName} could not not be found in the database";
            };

            return "You have added a new connection";
        }

        public async Task<List<Connection>> GetConnectionList([FromRoute] int Id, [FromBody] Connection connectModel)
        {
            await context.Connections.FindAsync(Id);
            return (List<Connection>)(IEnumerable)connectModel.Employees;
        }

        public IEnumerable<Connection> ConnectionList()
        {
            return context.Connections.ToList();
        }

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
