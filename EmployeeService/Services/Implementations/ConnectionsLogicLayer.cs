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
                Connection employeeConnection = await context.Connections.FirstOrDefaultAsync(x => x.Id == employeeId);
                ConnectionRequest connectionRequest = await context.ConnectionRequests.FirstOrDefaultAsync(x => x.ReceiverId == employeeId);

                if (employeeConnection == null)
                {
                    employeeConnection = new Connection
                    {
                        Id = employeeId,
                        Employees = new List<Employee>() { connection }
                    };


                    //updating the text in the RequestNotification column
                    connectionRequest.RequestNotification = "Accepted";

                    //context.ConnectionRequests.Remove(connectionRequest);

                    employee.Requests.Remove(connection);
                    employee.Connections.Add(connection);
                    await context.Connections.AddAsync(employeeConnection);
                    await context.SaveChangesAsync();
                    return "Added to Connection";
                }

                if (!employeeConnection.Employees.Contains(connection))
                {
                    employee.Requests.Remove(connection);
                    employee.Connections.Add(connection);
                    employeeConnection.Employees.Add(connection);
                    await context.SaveChangesAsync();
                    return "Added to Connection";
                }
                else
                {
                    return "Done";
                }
            }
            else
            {
                return "The employee or Customer could not be found in the database";
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployeeConnectionList(int Id)
        {
            Connection connectEmployee = await context.Connections.FirstOrDefaultAsync(x => x.Id == Id);
            return connectEmployee.Employees;
        }

        public async Task<IEnumerable<Connection>> ConnectionList()
        {
            var empList = await context.Connections.ToListAsync();
            return empList;
        }

        public async Task<string> DeleteFromConnection(int employeeId, int connectionId)
        {
            Employee employee = await context.Employees.FirstOrDefaultAsync(x => x.Id == employeeId);
            Employee connection = await context.Employees.FirstOrDefaultAsync(x => x.Id == connectionId);

            if (employee != null && connection != null)
            {
                Connection existingConnection = await context.Connections.FirstOrDefaultAsync(x => x.Id == employeeId);

                if (existingConnection == null)
                {
                    return "Cannot delete. Add to connection first";
                }
                else
                {
                    existingConnection.Employees.Remove(connection);
                    await context.SaveChangesAsync();
                    return "Employee successfully deleted from your connections";
                }
            }

            return "The Employee Id or Connection Id could not be found in the database";
        }
    }
}
