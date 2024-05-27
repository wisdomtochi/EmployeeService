using EmployeeService.DataAccess.Interfaces;
using EmployeeService.Domains;
using EmployeeService.Helpers;
using EmployeeService.Services.Interfaces;

namespace EmployeeService.Services.Implementations
{
    public class ConnectionService : IConnectionService
    {
        private readonly IGenericRepository<Connection> connectionGenericRepository;
        private readonly IGenericRepository<Employee> employeeGenericRepository;
        private readonly IGenericRepository<ConnectionRequest> connectionRequestGenericRepository;

        public ConnectionService(IGenericRepository<Connection> connectionGenericRepository, IGenericRepository<Employee> employeeGenericRepository,
                            IGenericRepository<ConnectionRequest> connectionRequestGenericRepository)
        {
            this.connectionGenericRepository = connectionGenericRepository;
            this.employeeGenericRepository = employeeGenericRepository;
            this.connectionRequestGenericRepository = connectionRequestGenericRepository;
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
                Employee employee = await employeeGenericRepository.ReadSingle(employeeId);
                Employee friend = await employeeGenericRepository.ReadSingle(friendId);

                if (employee == null) return Result.Failure("Unable to find Employee.");

                if (friend == null) return Result.Failure("Unable to find Friend-To-Be.");

                var allConnectionRequest = await connectionRequestGenericRepository.ReadAll();
                var checkIfConnectionRequestExist = allConnectionRequest.Where(x => x.ReceiverId == employeeId && x.SenderId == friendId && x.RequestNotification == "Pending");

                if (!checkIfConnectionRequestExist.Any()) return Result.Failure("Connection Not Found.");

                Connection addFriendToEmployeeFriendList = new()
                {
                    EmployeeId = employeeId,
                    FriendId = friendId
                };

                await connectionGenericRepository.Create(addFriendToEmployeeFriendList);
                await connectionGenericRepository.SaveChanges();
                //_ = checkIfConnectionRequestExist.FirstOrDefault().RequestNotification == "Added";
                return Result.Success("Friend Added Successfully.");
            }
            catch { throw; }

        }
        #endregion

        public async Task<Result> RemoveFromConnection(Guid employeeId, Guid friendId)
        {
            try
            {
                Employee employee = await employeeGenericRepository.ReadSingle(employeeId);
                Employee friend = await employeeGenericRepository.ReadSingle(friendId);

                if (employee == null) return Result.Failure("Unable to find Employee.");

                if (friend == null) return Result.Failure("Unable to find Friend-To-Be.");

                var allConnections = await connectionGenericRepository.ReadAll();

                var employeeConnection = allConnections.Where(x => x.EmployeeId == employeeId && x.FriendId == friendId);

                if (!employeeConnection.Any()) return Result.Failure("You do not have him as a connection.");

                await connectionGenericRepository.Delete(employeeId);
                await connectionGenericRepository.SaveChanges();
                return Result.Success("Removal Successful.");
            }
            catch { throw; }
        }

        public async Task<Result> ConnectionList(Guid employeeId)
        {
            try
            {
                Employee employee = await employeeGenericRepository.ReadSingle(employeeId);

                if (employee != null)
                {
                    var allConnections = await connectionGenericRepository.ReadAll();

                    var employeeConnections = allConnections.Select(x => x.EmployeeId == employeeId).ToList();

                    return Result.Success(employeeConnections);
                }

                return Result.Failure("Employee Not Found.");
            }
            catch { throw; }
        }
    }
}