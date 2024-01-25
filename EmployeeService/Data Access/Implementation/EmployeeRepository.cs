using EmployeeService.Data_Access.Interfaces;
using EmployeeService.Domains;

namespace EmployeeService.Data_Access
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IGenericRepository<Employee> employeeGenericRepository;

        public EmployeeRepository(IGenericRepository<Employee> employeeGenericRepository)
        {
            this.employeeGenericRepository = employeeGenericRepository;
        }


        public async Task<IEnumerable<Employee>> GetAllEmployee()
        {
            var employee = await employeeGenericRepository.ReadAll();
            return employee;
        }

        public async Task<Employee> GetEmployee(int id)
        {
            var employee = await employeeGenericRepository.ReadSingle(id);
            return employee;
        }

        public async Task<Employee> CreateEmployee(Employee employee)
        {
            await employeeGenericRepository.Create(employee);
            await employeeGenericRepository.SaveChanges();
            return employee;
        }

        public async Task DeleteEmployee(int employeeId)
        {
            await employeeGenericRepository.Delete(employeeId);
            await employeeGenericRepository.SaveChanges();
        }

        public async Task UpdateEmployee(Employee employeeModel)
        {
            var employee = await employeeGenericRepository.ReadSingle(employeeModel.Id);
            if (employee != null)
            {
                employee.Id = employeeModel.Id;
                employee.FirstName = employeeModel.FirstName;
                employee.LastName = employeeModel.LastName;
                employee.Gender = employeeModel.Gender;
                employee.Salary = employeeModel.Salary;

                employeeGenericRepository.Update(employee);
                await employeeGenericRepository.SaveChanges();
            }
        }
    }
}
