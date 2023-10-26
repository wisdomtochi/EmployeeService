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

        public async Task SendConnectionRequest(int receiverId, int senderId)
        {
            Employee receiver = await context.Employees.FirstOrDefaultAsync(x => x.Id == receiverId);
            Employee sender = await context.Employees.FirstOrDefaultAsync(x => x.Id == senderId);

            if (sender != null && receiver != null)
            {
                ConnectionRequest newRequest = await context.ConnectionRequests.FirstOrDefaultAsync(x => x.ReceiverId == receiverId);
                if (newRequest == null)
                {
                    newRequest = new ConnectionRequest
                    {
                        ReceiverId = receiverId,
                        SenderId = senderId,
                        RequestNotification = "Pending"
                    };

                    receiver.Requests.Add(sender);
                    await context.ConnectionRequests.AddAsync(newRequest);
                }
                else
                {
                    newRequest = new ConnectionRequest
                    {
                        SenderId = senderId,
                        RequestNotification = "Pending"
                    };
                    receiver.Requests.Add(sender);
                    await context.ConnectionRequests.AddAsync(newRequest);
                }

                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Employee>> GetConnectionRequestList(int Id)
        {
            Employee employee = await context.Employees.FindAsync(Id);

            ConnectionRequest connectionRequest = await context.ConnectionRequests.FindAsync(employee.Id);
            if (connectionRequest.RequestNotification == "Pending")
            {
                return employee.Requests;
            }

            //how to make the below line of code return a string rather than returning
            //the list of requests in the employee's table
            return employee.Requests;
        }

        public async Task<string> RemoveConnectionRequest(int employeeId, int requestId)
        {
            Employee employee = await context.Employees.FirstOrDefaultAsync(x => x.Id == employeeId);
            Employee request = await context.Employees.FirstOrDefaultAsync(x => x.Id == requestId);

            if (employee != null && request != null)
            {
                ConnectionRequest connectionRequest = await context.ConnectionRequests.FirstOrDefaultAsync(x => x.ReceiverId == employeeId);

                if (connectionRequest != null)
                {
                    context.ConnectionRequests.Remove(connectionRequest);
                    employee.Requests.Remove(request);
                    await context.SaveChangesAsync();
                    return "Request successfully removed from list.";
                }
                else
                {
                    return "You can't delete an employee's request when you don't a connection request message";
                }
            }
            else
            {
                return "Couldn't find Employee or the request Employee in the database.";
            }
        }
    }
}
