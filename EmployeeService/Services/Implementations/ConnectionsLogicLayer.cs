using EmployeeService.Data;
using EmployeeService.Domains;
using EmployeeService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                Connection existingConnection = await context.Connections.FirstOrDefaultAsync(x => x.Id == employeeId);

                if (existingConnection == null)
                {
                    existingConnection = new Connection
                    {
                        Id = employeeId,
                        Employees = new List<Employee>() { connection }
                    };

                    await context.Connections.AddAsync(existingConnection);
                    await context.SaveChangesAsync();
                }
                else
                {
                    if (!existingConnection.Employees.Contains(connection))
                    {
                        existingConnection.Employees.Add(connection);
                        await context.SaveChangesAsync();
                        return "Added to Connection";
                    }
                    else
                    {
                        return "Connection already exists";
                    }
                }

            };
            return " The employee or Customer could not be found in the database";
        }

        public async Task<IEnumerable<Employee>> GetEmployeeConnectionList(int Id)
        {
            Connection connectEmployee = await context.Connections.FirstOrDefaultAsync(x => x.Id == Id);
            var empList = connectEmployee.Employees;
            return empList;
        }

        public async Task<IEnumerable<Connection>> ConnectionList()
        {
            var empList = await context.Connections.ToListAsync();
            return empList;
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
