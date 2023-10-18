using EmployeeService.Data;
using EmployeeService.Domains;
using EmployeeService.Services.Interfaces;

namespace EmployeeService.Services.Implementations
{
    public class ConnectionsLogicLayer : IConnectionsLogicLayer
    {
        private readonly EmployeeDbContext context;
        private List<Employee> _listOfConnections;

        public ConnectionsLogicLayer(EmployeeDbContext context)
        {
            _listOfConnections = new List<Employee>();
            this.context = context;
        }

        public async Task<string> AddToConnection(Employee employeeModel)
        {
            var emp = await context.Employees.FindAsync(employeeModel.Id);

            if (emp != null)
            {
                _listOfConnections.Add(emp);

                foreach (Employee connection in _listOfConnections)
                {
                    return connection.FirstName;
                }
            }

            return $"{employeeModel.FirstName} {employeeModel.LastName} could not not be found in the database";
        }

        //public Employee SeeConnectionsRequest(Employee employee)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
