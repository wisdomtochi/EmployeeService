using EmployeeService.Data;
using EmployeeService.Domains;
using EmployeeService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Services.Implementations
{
    public class ConnectionRequestLogicLayer : IConnectionRequestLogicLayer
    {
        private readonly EmployeeDbContext context;

        public ConnectionRequestLogicLayer(EmployeeDbContext context)
        {
            this.context = context;
        }

        public async Task SendConnectionRequest(int employeeId, int requestId)
        {
            Employee employee = await context.Employees.FirstOrDefaultAsync(x => x.Id == employeeId);
            Employee request = await context.Employees.FirstOrDefaultAsync(x => x.Id == requestId);

            if (employee != null && request != null)
            {
                ConnectionRequest newRequest = await context.ConnectionRequests.FirstOrDefaultAsync(x => x.ReceiverId == requestId);
                if (newRequest == null)
                {
                    newRequest = new ConnectionRequest
                    {
                        ReceiverId = requestId,
                        SenderId = employeeId,
                        RequestNotification = "Pending"
                    };

                    await context.ConnectionRequests.AddAsync(newRequest);
                    await context.SaveChangesAsync();
                }
                else
                {
                    newRequest = new ConnectionRequest
                    {
                        SenderId = employeeId,
                        RequestNotification = "Pending"
                    };
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<IEnumerable<Employee>> GetConnectionRequestList(int id)
        {
            Employee employee = await context.Employees.FindAsync(id);
            //1. how to check for nullability and return "couldn't find employee" as a string 

            //2. code to check if the requestnotification message is Pending to return the employees with that message
            //rather than removing the employees that their requests have been accepted from the table totally 

            //ConnectionRequest connectionRequest = await context.ConnectionRequests.FindAsync(id);
            //if (connectionRequest.RequestNotification == "Pending")
            //{
            //    return (IEnumerable<Employee>)employee.Requests;
            //}
            return (IEnumerable<Employee>)employee.Requests;
        }
    }
}
