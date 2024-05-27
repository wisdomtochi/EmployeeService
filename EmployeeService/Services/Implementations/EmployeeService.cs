using EmployeeService.DataAccess.Interfaces;
using EmployeeService.Domains;
using EmployeeService.Helpers;
using EmployeeService.Services.Interfaces;

namespace EmployeeService.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IGenericRepository<Employee> employeeGenericRepository;

        public EmployeeService(IGenericRepository<Employee> employeeGenericRepository)
        {
            this.employeeGenericRepository = employeeGenericRepository;
        }

        public async Task<Result> GetAllEmployee()
        {
            var employeeList = await employeeGenericRepository.ReadAll();

            if (!employeeList.Any()) return Result.Failure("No Employee Found.");

            return Result.Success(employeeList);
        }

        public async Task<Result> GetEmployee(Guid id)
        {
            var employee = await employeeGenericRepository.ReadSingle(id);

            if (employee == null) return Result.Failure("Employee Not Found.");

            return Result.Success(employee);
        }

        public async Task<Result> CreateEmployee(Employee employee)
        {
            try
            {
                Employee newEmployee = new()
                {
                    Id = Guid.NewGuid(),
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Gender = employee.Gender,
                    Salary = employee.Salary
                };

                await employeeGenericRepository.Create(newEmployee);
                await employeeGenericRepository.SaveChanges();
                return Result.Success(newEmployee, "Created Successfully.");
            }
            catch { throw; }
        }

        public async Task<Result> DeleteEmployee(Guid employeeId)
        {
            Employee employee = await employeeGenericRepository.ReadSingle(employeeId);

            if (employee != null)
            {
                await employeeGenericRepository.Delete(employeeId);
                await employeeGenericRepository.SaveChanges();
                return Result.Success("Deleted Successfully.");
            }
            return Result.Failure("Unable to Delete.");
        }

        public async Task<Result> UpdateEmployee(Guid id, Employee employeeModel)
        {
            var employee = await employeeGenericRepository.ReadSingle(id);
            if (employee != null)
            {
                employee.FirstName = employeeModel.FirstName;
                employee.LastName = employeeModel.LastName;
                employee.Gender = employeeModel.Gender;
                employee.Salary = employeeModel.Salary;

                employeeGenericRepository.Update(employee);
                await employeeGenericRepository.SaveChanges();
                return Result.Success("Updated Successfully.");
            }
            return Result.Failure("Unable to Update.");
        }
    }
}
