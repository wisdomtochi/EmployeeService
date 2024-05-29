using EmployeeService.DataAccess.Interfaces;
using EmployeeService.Domains;
using EmployeeService.Enums;
using EmployeeService.Helpers;
using EmployeeService.Services.Interfaces;

namespace EmployeeService.Services.Implementations
{
    public class ConnectionService : IConnectionService
    {
        private readonly IUnitofWork<Connection> connectionUoW;
        private readonly IUnitofWork<Employee> employeeUoW;
        private readonly IUnitofWork<ConnectionRequest> connectionRequestUoW;

        public ConnectionService(IUnitofWork<Connection> connectionUoW,
                                 IUnitofWork<Employee> employeeUoW,
                                 IUnitofWork<ConnectionRequest> connectionRequestUoW)
        {
            this.connectionUoW = connectionUoW;
            this.employeeUoW = employeeUoW;
            this.connectionRequestUoW = connectionRequestUoW;
        }

        #region ADD TO CONNECTIONS
        /// <summary>
        /// Add an employee to your connections
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task<Result> AddToConnection(Guid employeeId, Guid friendId)
        {
            try
            {
                Employee employee = await employeeUoW.Repository.ReadSingle(employeeId);
                Employee friend = await employeeUoW.Repository.ReadSingle(friendId);

                if (employee == null) return Result.Failure("Unable to find Employee.");

                if (friend == null) return Result.Failure("Unable to find Friend-To-Be.");

                var allConnectionRequest = await connectionRequestUoW.Repository.ReadAll();

                if (!allConnectionRequest.Any(x => x.ReceiverId == employeeId && x.SenderId == friendId && x.RequestNotification == "Pending")) return Result.Failure("Connection Not Found.");

                Connection addFriendToEmployeeFriendList = new()
                {
                    EmployeeId = employeeId,
                    FriendId = friendId
                };

                ConnectionRequest request = await connectionRequestUoW.Repository.ReadSingle(employeeId);
                request.RequestNotification = EnumsImplementation.ConfirmationMessage(ConnectionRequestMessagesEnum.RequestAccepted);

                connectionRequestUoW.Repository.Update(request);
                await connectionUoW.Repository.Create(addFriendToEmployeeFriendList);
                await connectionUoW.SaveChangesAsync();
                return Result.Success("Friend Added Successfully.");
            }
            catch { throw; }

        }
        #endregion

        #region REMOVE FROM CONNECTION
        public async Task<Result> RemoveFromConnection(Guid employeeId, Guid friendId)
        {
            try
            {
                Employee employee = await employeeUoW.Repository.ReadSingle(employeeId);
                Employee friend = await employeeUoW.Repository.ReadSingle(friendId);

                if (employee == null) return Result.Failure("Unable to find Employee.");

                if (friend == null) return Result.Failure("Unable to find Friend-To-Be.");

                var allConnections = await connectionUoW.Repository.ReadAll();

                var employeeConnection = allConnections.Where(x => x.EmployeeId == employeeId && x.FriendId == friendId);

                if (!employeeConnection.Any()) return Result.Failure("You do not have him as a connection.");

                await connectionUoW.Repository.Delete(employeeId);
                await connectionUoW.SaveChangesAsync();
                return Result.Success("Removal Successful.");
            }
            catch { throw; }
        }
        #endregion

        #region CONNECTION LIST
        public async Task<Result> ConnectionList(Guid employeeId)
        {
            try
            {
                Employee employee = await employeeUoW.Repository.ReadSingle(employeeId);

                if (employee != null)
                {
                    var allConnections = await connectionUoW.Repository.ReadAll();

                    var employeeConnections = allConnections.Select(x => x.EmployeeId == employeeId).ToList();

                    return Result.Success(employeeConnections);
                }

                return Result.Failure("Employee Not Found.");
            }
            catch { throw; }
        }
        #endregion
    }
}