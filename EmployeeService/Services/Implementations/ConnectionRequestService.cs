using EmployeeService.DataAccess.Interfaces;
using EmployeeService.Domains;
using EmployeeService.Helpers;
using EmployeeService.Services.Interfaces;

namespace EmployeeService.Services.Implementations
{
    public class ConnectionRequestService : IConnectionRequestService
    {
        private readonly IUnitofWork<ConnectionRequest> connectionRequestUoW;
        private readonly IUnitofWork<Employee> employeeUoW;

        public ConnectionRequestService(IUnitofWork<ConnectionRequest> connectionRequestUoW,
                        IUnitofWork<Employee> employeeUoW)
        {
            this.connectionRequestUoW = connectionRequestUoW;
            this.employeeUoW = employeeUoW;
        }

        public async Task<Result> SendConnectionRequest(Guid receiverId, Guid senderId)
        {
            Employee receiver = await employeeUoW.Repository.ReadSingle(receiverId);
            Employee sender = await employeeUoW.Repository.ReadSingle(senderId);

            if (receiver == null) return Result.Failure("Unable to find Receiver.");

            if (sender == null) return Result.Failure("Unable to find Sender.");

            ConnectionRequest request = new()
            {
                ReceiverId = receiverId,
                SenderId = senderId,
                RequestNotification = "Pending"
            };

            await connectionRequestUoW.Repository.Create(request);
            await connectionRequestUoW.SaveChangesAsync();
            return Result.Success("Created Successfully.");
        }
    }


}
