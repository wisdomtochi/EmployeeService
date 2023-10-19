using EmployeeService.Data;
using EmployeeService.Domains;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Data_Access
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext context;

        public EmployeeRepository(EmployeeDbContext context)
        {
            this.context = context;
        }


        public async Task<IEnumerable<Employee>> GetAllEmployee()
        {
            var employee = await context.Employees.ToListAsync();

            return employee;
        }

        public async Task<Employee> GetEmployee(int id)
        {
            var thisReturn = await context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            return thisReturn;
        }

        public async Task<Employee> CreateEmployee(Employee employee)
        {
            await context.Employees.AddAsync(employee);
            await context.SaveChangesAsync();
            return employee;
        }

        public async Task DeleteEmployee(int employeeId)
        {
            var employee = new Employee() { Id = employeeId };
            context.Employees.Remove(employee);
            await context.SaveChangesAsync();
        }

        public async Task UpdateEmployee(Employee employeeModel)
        {
            var employee = await context.Employees.FindAsync(employeeModel.Id);
            if (employee != null)
            {
                employee.Id = employeeModel.Id;
                employee.FirstName = employeeModel.FirstName;
                employee.LastName = employeeModel.LastName;
                employee.Gender = employeeModel.Gender;
                employee.Salary = employeeModel.Salary;

                await context.SaveChangesAsync();
            }
        }
    }
}
