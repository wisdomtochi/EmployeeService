using EmployeeService.DataAccess.Interfaces;
using EmployeeService.Domains;
using EmployeeService.Helpers;
using EmployeeService.Services.Interfaces;

namespace EmployeeService.Services.Implementations
{
    public class ConnectionRequestService : IConnectionRequestService
    {
        private readonly IGenericRepository<ConnectionRequest> connectionRequestGenericRepository;
        private readonly IGenericRepository<Employee> employeeGenericRepository;

        public ConnectionRequestService(IGenericRepository<ConnectionRequest> connectionRequestGenericRepository,
                        IGenericRepository<Employee> employeeGenericRepository)
        {
            this.connectionRequestGenericRepository = connectionRequestGenericRepository;
            this.employeeGenericRepository = employeeGenericRepository;
        }

        public async Task<Result> SendConnectionRequest(Guid receiverId, Guid senderId)
        {
            Employee receiver = await employeeGenericRepository.ReadSingle(receiverId);
            Employee sender = await employeeGenericRepository.ReadSingle(senderId);

            if (receiver == null) return Result.Failure("Unable to find Receiver.");

            if (sender == null) return Result.Failure("Unable to find Sender.");

            ConnectionRequest request = new()
            {
                ReceiverId = receiverId,
                SenderId = senderId,
                RequestNotification = "Pending"
            };

            await connectionRequestGenericRepository.Create(request);
            await connectionRequestGenericRepository.SaveChanges();
            return Result.Success("Created Successfully.");
        }
    }


}
