using EmployeeService.DataAccess.Interfaces;
using EmployeeService.Domains;
using EmployeeService.DTO.Read;
using EmployeeService.DTO.Write;
using EmployeeService.Helpers;
using EmployeeService.Services.Interfaces;

namespace EmployeeService.Services.Implementations
{
    public class EmployeeServiceLogic : IEmployeeServiceLogic
    {
        private readonly IUnitofWork<Employee> employeeUoW;

        public EmployeeServiceLogic(IUnitofWork<Employee> employeeUoW)
        {
            this.employeeUoW = employeeUoW;
        }

        public async Task<Result<List<Employee>>> GetAllEmployee()
        {
            var employeeList = await employeeUoW.Repository.ReadAll();

            if (!employeeList.Any()) return Result.Failure<List<Employee>>("No Employee Found.");

            return Result.Success(employeeList.ToList());
        }

        public async Task<Result<EmployeeDTO>> GetEmployee(Guid id)
        {
            var employee = await employeeUoW.Repository.ReadSingle(id);

            if (employee == null) return Result.Failure<EmployeeDTO>("Employee Not Found.");

            return Result.Success(employee);
        }

        public async Task<Result<Employee>> CreateEmployee(EmployeeDTOw employee)
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

                await employeeUoW.Repository.Create(newEmployee);
                await employeeUoW.SaveChangesAsync();
                return Result.Success(newEmployee, "Employee Created Successfully.");
            }
            catch { throw; }
        }

        public async Task<Result> DeleteEmployee(Guid employeeId)
        {
            Employee employee = await employeeUoW.Repository.ReadSingle(employeeId);

            if (employee != null)
            {
                await employeeUoW.Repository.Delete(employeeId);
                await employeeUoW.SaveChangesAsync();
                return Result.Success("Deleted Successfully.");
            }
            return Result.Failure("Unable to Delete.");
        }

        public async Task<Result> UpdateEmployee(Guid id, EmployeeDTOw employeeModel)
        {
            var employee = await employeeUoW.Repository.ReadSingle(id);
            if (employee != null)
            {
                employee.FirstName = employeeModel.FirstName;
                employee.LastName = employeeModel.LastName;
                employee.Gender = employeeModel.Gender;
                employee.Salary = employeeModel.Salary;

                employeeUoW.Repository.Update(employee);
                await employeeUoW.SaveChangesAsync();
                return Result.Success("Updated Successfully.");
            }
            return Result.Failure("Unable to Update.");
        }
    }
}
