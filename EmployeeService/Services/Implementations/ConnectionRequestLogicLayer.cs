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

        public async Task SendConnectionRequest(int senderId, int receiverId)
        {
            Employee sender = await context.Employees.FirstOrDefaultAsync(x => x.Id == senderId);
            Employee receiver = await context.Employees.FirstOrDefaultAsync(x => x.Id == receiverId);

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

                    receiver.Requests.Add(sender.Id);
                    await context.ConnectionRequests.AddAsync(newRequest);
                    await context.SaveChangesAsync();
                }
                else
                {
                    newRequest = new ConnectionRequest
                    {
                        SenderId = senderId,
                        RequestNotification = "Pending"
                    };
                    receiver.Requests.Add(sender.Id);
                    await context.ConnectionRequests.AddAsync(newRequest);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<IEnumerable<int>> GetConnectionRequestList(int Id)
        {
            Employee employee = await context.Employees.FindAsync(Id);

            //code to check if the requestnotification message is Pending to return the employees with that message
            //rather than removing the employees that their requests have been accepted from the table totally 

            ConnectionRequest connectionRequest = await context.ConnectionRequests.FindAsync(employee.Id);
            if (connectionRequest.RequestNotification == "Pending")
            {
                return employee.Requests;
            }

            //how to make the below line of code return a string rather than
            //the list of requests in the employees table
            return employee.Requests;
        }
    }
}
