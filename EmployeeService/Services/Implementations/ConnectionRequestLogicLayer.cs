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

        public async Task<IEnumerable<ConnectionRequest>> GetAllConnectionRequest()
        {
            var result = await context.ConnectionRequests.ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Employee>> GetConnectionRequestList(int id)
        {
            Employee employee = await context.Employees.FindAsync(id);
            //how to check for nullability and return "couldn't find employee" as a string 
            if (employee == null)
            {
                return null;
            }
            return (IEnumerable<Employee>)employee.Requests;
        }
    }
}
