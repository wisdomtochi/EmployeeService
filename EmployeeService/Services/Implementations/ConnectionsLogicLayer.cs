using EmployeeService.Data;
using EmployeeService.Domains;
using EmployeeService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task<string> AddToConnection(int employeeId, int connectionId)
        {
            Employee employee = await context.Employees.FirstOrDefaultAsync(x => x.Id == employeeId);
            Employee connection = await context.Employees.FirstOrDefaultAsync(x => x.Id == connectionId);

            if (employee != null && connection != null)
            {


                if (!employee.Connections.Contains(connection))
                {
                    employee.Connections.Add(connection);
                    await context.SaveChangesAsync();
                }
                else
                {
                    return "Already in your connections";
                }
            };
            return " The employee or Customer could not be found in the database";
        }

        public async Task<List<Connection>> GetConnectionList(int Id)
        {
            Connection connectEmployee = await context.Connections.FirstOrDefaultAsync(x => x.Id == Id);
            return (List<Connection>)(IEnumerable)connectEmployee.Employees;
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
