using EmployeeService.Data_Access.Interfaces;
using EmployeeService.Domains;
using EmployeeService.DTO.Read;
using EmployeeService.Services.Interfaces;

namespace EmployeeService.Services.Implementations
{
    public class ConnectionsLogicLayer : IConnectionsLogicLayer
    {
        private readonly IGenericRepository<Connection> connectionGenericRepository;
        private readonly IGenericRepository<Employee> employeeGenericRepository;
        private readonly IGenericRepository<ConnectionRequest> connectionRequestGenericRepository;

        public ConnectionsLogicLayer(IGenericRepository<Connection> connectionGenericRepository, IGenericRepository<Employee> employeeGenericRepository,
                            IGenericRepository<ConnectionRequest> connectionRequestGenericRepository)
        {
            this.connectionGenericRepository = connectionGenericRepository;
            this.employeeGenericRepository = employeeGenericRepository;
            this.connectionRequestGenericRepository = connectionRequestGenericRepository;
        }

        public async Task<string> AddToConnection(int employeeId, int connectionId)
        {
            Employee employee = await employeeGenericRepository.ReadSingle(employeeId);
            Employee connection = await employeeGenericRepository.ReadSingle(connectionId);

            if (employee != null && connection != null)
            {
                Connection employeeConnection = await connectionGenericRepository.ReadSingle(employeeId);
                ConnectionRequest connectionRequest = await connectionRequestGenericRepository.ReadSingle(employeeId);

                Connection existingConnection = await connectionGenericRepository.ReadSingle(connectionId);

                if (existingConnection == null)
                {
                    existingConnection = new Connection
                    {
                        Id = connectionId
                    };

                    await connectionGenericRepository.Create(existingConnection);
                    await connectionGenericRepository.SaveChanges();
                }

                if (employeeConnection == null)
                {
                    employeeConnection = new Connection
                    {
                        Id = employeeId,
                        Employees = new List<Employee>() { connection }
                    };


                    //updating the text in the RequestNotification column
                    connectionRequest.RequestNotification = EnumsImplementation.ConfirmationMessage(ConnectionMessagesEnum.Accepted);

                    //context.ConnectionRequests.Remove(connectionRequest);

                    employee.Requests.Remove(connection);
                    employee.Connections.Add(existingConnection);
                    await connectionGenericRepository.Create(employeeConnection);
                    await connectionGenericRepository.SaveChanges();
                    return EnumsImplementation.ConfirmationMessage(ConnectionMessagesEnum.AddedtoConnection);
                }

                if (employeeConnection.Employees.Contains(connection))
                {
                    return EnumsImplementation.ConfirmationMessage(ConnectionMessagesEnum.AlreadyInYourConnectionList);
                }
                else
                {
                    employeeConnection.Employees.Add(connection);
                    employee.Requests.Remove(connection);
                    employee.Connections.Add(existingConnection);
                    await employeeGenericRepository.SaveChanges();
                    return EnumsImplementation.ConfirmationMessage(ConnectionMessagesEnum.AddedtoConnection);
                }
            }
            else
            {
                return EnumsImplementation.ConfirmationMessage(ConnectionMessagesEnum.CouldNotBeFound);
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployeeConnectionList(int Id)
        {
            Connection connectEmployee = await connectionGenericRepository.ReadSingle(Id);
            return connectEmployee.Employees;
        }

        public async Task<IEnumerable<Connection>> ConnectionList()
        {
            var empList = await connectionGenericRepository.ReadAll();
            return empList;
        }

        public async Task<string> DeleteFromConnection(int employeeId, int connectionId)
        {
            Employee employee = await employeeGenericRepository.ReadSingle(employeeId);
            Employee connection = await employeeGenericRepository.ReadSingle(connectionId);

            if (employee != null && connection != null)
            {
                Connection existingConnection = await connectionGenericRepository.ReadSingle(employeeId);

                if (existingConnection == null)
                {
                    return EnumsImplementation.ConfirmationMessage(ConnectionMessagesEnum.CannotDelete);
                }
                else
                {
                    existingConnection.Employees.Remove(connection);
                    await connectionGenericRepository.SaveChanges();
                    return EnumsImplementation.ConfirmationMessage(ConnectionMessagesEnum.EmployeeDeleted);
                }
            }

            return EnumsImplementation.ConfirmationMessage(ConnectionMessagesEnum.CouldNotBeFound);
        }
    }
}
