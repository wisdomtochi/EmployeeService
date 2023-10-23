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
                ConnectionRequest connectionRequest = await context.ConnectionRequests.FirstOrDefaultAsync(x => x.Id == requestId);
                if (connectionRequest == null)
                {
                    connectionRequest = new ConnectionRequest
                    {
                        Id = requestId,
                        Employees = new List<Employee> { employee },
                        RequestNotification = "Pending"
                    };

                    await context.ConnectionRequests.AddAsync(connectionRequest);
                    await context.SaveChangesAsync();
                }
                else if (!connectionRequest.Employees.Contains(employee))
                {
                    connectionRequest.Employees.Add(employee);
                    connectionRequest.RequestNotification = "Pending";
                    await context.SaveChangesAsync();
                }
            }
        }


    }
}
