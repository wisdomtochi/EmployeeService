using EmployeeService.DataAccess.Interfaces;
using EmployeeService.Domains;
using EmployeeService.DTO.Read;
using EmployeeService.Enums;
using EmployeeService.Helpers;
using EmployeeService.Mappers;
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

        public async Task<Result<List<RequestDTO>>> GetConnectionRequests(Guid Id)
        {
            try
            {
                var requestExist = await connectionRequestUoW.Repository.ReadAll();
                var employeeRequests = requestExist.Where(emp => emp.ReceiverId == Id);

                if (employeeRequests == null) return Result.Failure<List<RequestDTO>>($"Unable To Find Person With Id = {Id}.");

                var employeeRequestlist = employeeRequests.Where(e => e.RequestNotification == "Pending");

                if (!employeeRequestlist.Any()) return Result.Failure<List<RequestDTO>>("No Request Found.");

                var requests = Map.Requests(employeeRequestlist);

                return Result.Success(requests);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Result<string>> SendConnectionRequest(Guid receiverId, Guid senderId)
        {
            try
            {
                Employee receiver = await employeeUoW.Repository.ReadSingle(receiverId);
                if (receiver == null) return Result.Failure<string>("Unable to find Receiver.");

                Employee sender = await employeeUoW.Repository.ReadSingle(senderId);
                if (sender == null) return Result.Failure<string>("Unable to find Sender.");

                var connectionList = await connectionRequestUoW.Repository.ReadAll();
                if (connectionList.Any(r => r.ReceiverId == receiverId && r.SenderId == senderId)) return Result.Failure<string>("Request Already Exist.");

                ConnectionRequest request = new()
                {
                    ReceiverId = receiverId,
                    SenderId = senderId,
                    RequestNotification = EnumsImplementation.ConfirmationMessage(RequestEnum.Pending)
                };

                await connectionRequestUoW.Repository.Create(request);
                await connectionRequestUoW.SaveChangesAsync();
                return Result.Success(EnumsImplementation.ConfirmationMessage(RequestEnum.RequestSent));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Result<string>> RemoveConnectionRequest(Guid senderId, Guid receiverId)
        {
            try
            {
                Employee receiver = await employeeUoW.Repository.ReadSingle(receiverId);
                if (receiver == null) return Result.Failure<string>("Unable to find Receiver.");

                Employee sender = await employeeUoW.Repository.ReadSingle(senderId);
                if (sender == null) return Result.Failure<string>("Unable to find Sender.");

                var allConnectionRequests = await connectionRequestUoW.Repository.ReadAll();

                var requestExist = allConnectionRequests.Where(r => r.SenderId == senderId && r.ReceiverId == receiverId).FirstOrDefault();
                if (requestExist == null) return Result.Failure<string>("Request Not Found.");

                await connectionRequestUoW.Repository.Delete(requestExist);
                await connectionRequestUoW.SaveChangesAsync();
                return Result.Success("Request Cancelled Successfully.");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }


}
