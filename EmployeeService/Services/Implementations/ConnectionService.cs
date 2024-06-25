using EmployeeService.DataAccess.Interfaces;
using EmployeeService.Domains;
using EmployeeService.DTO.Read;
using EmployeeService.Enums;
using EmployeeService.Helpers;
using EmployeeService.Mappers;
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
        public async Task<Result<string>> AddToConnection(Guid employeeId, Guid friendId)
        {
            try
            {
                Employee employee = await employeeUoW.Repository.ReadSingle(employeeId);
                if (employee == null) return Result.Failure<string>("Unable to find Employee.");

                Employee friend = await employeeUoW.Repository.ReadSingle(friendId);
                if (friend == null) return Result.Failure<string>("Unable to find Friend-To-Be.");

                var connections = await connectionUoW.Repository.ReadAll();
                if (connections.Any(c => c.EmployeeId == employeeId && c.FriendId == friendId)) return Result.Failure<string>(EnumsImplementation.ConfirmationMessage(ConnectionEnum.AlreadyInYourConnectionList));

                var allConnectionRequest = await connectionRequestUoW.Repository.ReadAll();
                if (!allConnectionRequest.Any(x => x.ReceiverId == employeeId && x.SenderId == friendId && x.RequestNotification == "Pending")) return Result.Failure<string>(EnumsImplementation.ConfirmationMessage(ConnectionEnum.CannotAdd));

                Connection addFriendToEmployeeFriendList = new()
                {
                    EmployeeId = employeeId,
                    FriendId = friendId
                };

                var request = allConnectionRequest.Where(c => c.ReceiverId == employeeId).FirstOrDefault();

                await connectionRequestUoW.Repository.Delete(request);
                await connectionUoW.Repository.Create(addFriendToEmployeeFriendList);
                await connectionUoW.SaveChangesAsync();
                return Result.Success($"You have added {friend.FirstName} {friend.LastName} to your connections.");
            }
            catch (Exception)
            { throw; }

        }
        #endregion

        #region REMOVE FROM CONNECTION
        public async Task<Result<string>> RemoveFromConnection(Guid employeeId, Guid friendId)
        {
            try
            {
                Employee employee = await employeeUoW.Repository.ReadSingle(employeeId);
                if (employee == null) return Result.Failure<string>("Unable to find Employee.");

                Employee friend = await employeeUoW.Repository.ReadSingle(friendId);
                if (friend == null) return Result.Failure<string>("Unable to find Friend-To-Be.");

                var allConnections = await connectionUoW.Repository.ReadAll();

                var employeeConnection = allConnections.Where(x => x.EmployeeId == employeeId && x.FriendId == friendId).FirstOrDefault();
                if (employeeConnection == null) return Result.Failure<string>(EnumsImplementation.ConfirmationMessage(ConnectionEnum.CouldNotBeFound));

                await connectionUoW.Repository.Delete(employeeConnection);
                await connectionUoW.SaveChangesAsync();
                return Result.Success(EnumsImplementation.ConfirmationMessage(ConnectionEnum.EmployeeDeleted));
            }
            catch (Exception)
            { throw; }
        }
        #endregion

        #region CONNECTION LIST
        public async Task<Result<List<ConnectionDTO>>> ConnectionList(Guid employeeId)
        {
            try
            {
                var allConnections = await connectionUoW.Repository.ReadAll();

                var employeeConnections = allConnections.Where(x => x.EmployeeId == employeeId);
                if (employeeConnections == null) return Result.Failure<List<ConnectionDTO>>($"Unable To Find Person With Id = {employeeId}.");

                if (!employeeConnections.Any()) return Result.Failure<List<ConnectionDTO>>("No Connection Found.");

                var connections = Map.Connections(employeeConnections);
                return Result.Success(connections);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}