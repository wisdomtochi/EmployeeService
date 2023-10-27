using EmployeeService.Data_Access.Interfaces;
using EmployeeService.Domains;
using EmployeeService.DTO;
using EmployeeService.Services.Interfaces;

namespace EmployeeService.Services.Implementations
{
    public class ConnectionRequestLogicLayer : IConnectionRequestLogicLayer
    {
        private readonly IGenericRepository<ConnectionRequest> connectionRequestGenericRepository;
        private readonly IGenericRepository<Employee> employeeGenericRepository;

        public ConnectionRequestLogicLayer(IGenericRepository<ConnectionRequest> connectionRequestGenericRepository,
                        IGenericRepository<Employee> employeeGenericRepository)
        {
            this.connectionRequestGenericRepository = connectionRequestGenericRepository;
            this.employeeGenericRepository = employeeGenericRepository;
        }

        public async Task SendConnectionRequest(int receiverId, int senderId)
        {
            Employee receiver = await employeeGenericRepository.ReadSingle(receiverId);
            Employee sender = await employeeGenericRepository.ReadSingle(senderId);

            if (sender != null && receiver != null)
            {
                ConnectionRequest newRequest = await connectionRequestGenericRepository.ReadSingle(receiverId);
                if (newRequest == null)
                {
                    newRequest = new ConnectionRequest
                    {
                        ReceiverId = receiverId,
                        SenderId = senderId,
                        RequestNotification = EnumsImplementation.ConfirmationMessage(ConnectionRequestMessagesEnum.Pending)
                    };

                    receiver.Requests.Add(sender);
                    await connectionRequestGenericRepository.Create(newRequest);
                }
                else
                {
                    newRequest = new ConnectionRequest
                    {
                        ReceiverId = receiverId,
                        SenderId = senderId,
                        RequestNotification = EnumsImplementation.ConfirmationMessage(ConnectionRequestMessagesEnum.Pending)
                    };
                    receiver.Requests.Add(sender);
                    await connectionRequestGenericRepository.Create(newRequest);
                }

                await connectionRequestGenericRepository.SaveChanges();
            }
        }

        public async Task<IEnumerable<Employee>> GetConnectionRequestList(int Id)
        {
            Employee employee = await employeeGenericRepository.ReadSingle(Id);

            ConnectionRequest connectionRequest = await connectionRequestGenericRepository.ReadSingle(Id);
            if (connectionRequest.RequestNotification == EnumsImplementation.ConfirmationMessage(ConnectionRequestMessagesEnum.Pending))
            {
                return employee.Requests;
            }

            //how to make the below line of code return a string rather than returning
            //the list of requests in the employee's table
            return employee.Requests;
        }

        public async Task<string> RemoveConnectionRequest(int employeeId, int requestId)
        {
            Employee employee = await employeeGenericRepository.ReadSingle(employeeId);
            Employee request = await employeeGenericRepository.ReadSingle(requestId);

            if (employee != null && request != null)
            {
                ConnectionRequest connectionRequest = await connectionRequestGenericRepository.ReadSingle(requestId);

                if (connectionRequest != null)
                {
                    employee.Requests.Remove(request);
                    await connectionRequestGenericRepository.Delete(requestId);
                    await connectionRequestGenericRepository.SaveChanges();
                    return EnumsImplementation.ConfirmationMessage(ConnectionRequestMessagesEnum.RequestRemoved);
                }
                else
                {
                    return EnumsImplementation.ConfirmationMessage(ConnectionRequestMessagesEnum.CannotDeleteRequest);
                }
            }
            else
            {
                return EnumsImplementation.ConfirmationMessage(ConnectionRequestMessagesEnum.CouldNotBeFound);
            }
        }
    }
}
