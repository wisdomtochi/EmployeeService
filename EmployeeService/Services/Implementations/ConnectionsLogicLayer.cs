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

            if (employee != null)
            {
                Connection existingConnection = await context.Connections.FirstOrDefaultAsync(x => x.Id == employeeId);

                if (existingConnection == null)
                {
                    existingConnection = new Connection
                    {
                        Id = employeeId,
                        Employees = new List<Employee>()
                    };

                    await context.Connections.AddAsync(existingConnection);
                    await context.SaveChangesAsync();
                }

                if (!existingConnection.Employees.Contains(connection))
                {
                    existingConnection.Employees.Add(connection);
                    await context.SaveChangesAsync();
                }
                else
                {
                    return "Connection already exists";
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
