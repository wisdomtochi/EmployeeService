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

                Connection newConnection = await connectionGenericRepository.ReadSingle(connectionId);

                if (newConnection == null)
                {
                    newConnection = new Connection
                    {
                        Id = connectionId
                    };

                    await connectionGenericRepository.Create(newConnection);
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
                    employee.Connections.Add(newConnection);
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
                    employee.Requests.Remove(connection);
                    employee.Connections.Add(newConnection);
                    employeeConnection.Employees.Add(connection);
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
                Connection employeeConnection = await connectionGenericRepository.ReadSingle(employeeId);
                Connection otherPersonConnectionObject = await connectionGenericRepository.ReadSingle(connectionId);

                if (employeeConnection == null)
                {
                    return EnumsImplementation.ConfirmationMessage(ConnectionMessagesEnum.CannotDelete);
                }
                else
                {
                    employee.Connections.Remove(otherPersonConnectionObject);
                    employeeConnection.Employees.Remove(connection);
                    await connectionGenericRepository.SaveChanges();
                    return EnumsImplementation.ConfirmationMessage(ConnectionMessagesEnum.EmployeeDeleted);
                }
            }

            return EnumsImplementation.ConfirmationMessage(ConnectionMessagesEnum.CouldNotBeFound);
        }
    }
}
