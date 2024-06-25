using EmployeeService.DataAccess.Interfaces;
using EmployeeService.Domains;
using EmployeeService.DTO.Read;
using EmployeeService.DTO.Write;
using EmployeeService.Enums;
using EmployeeService.Helpers;
using EmployeeService.Mappers;
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

        public async Task<Result<List<EmployeeDTO>>> GetAllEmployee()
        {
            var employeeList = await employeeUoW.Repository.ReadAll();

            if (!employeeList.Any()) return Result.Failure<List<EmployeeDTO>>("No Employee Found.");

            List<EmployeeDTO> employees = Map.Employees(employeeList);

            return Result.Success(employees, "List of Employees.");
        }

        public async Task<Result<EmployeeDTO>> GetEmployee(Guid id)
        {
            var EmployeeExist = await employeeUoW.Repository.ReadSingle(id);

            if (EmployeeExist == null) return Result.Failure<EmployeeDTO>("Employee Not Found.");

            var employee = Map.Employees(new List<Employee> { EmployeeExist }).FirstOrDefault();

            return Result.Success(employee);
        }

        public async Task<Result<EmployeeDTO>> CreateEmployee(EmployeeDTOw employee)
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

                var employeeDTO = Map.Employees(new List<Employee> { newEmployee }).FirstOrDefault();
                return Result.Success(employeeDTO, "Employee Created Successfully.");
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
            return Result.Failure("Employee Not Found.");
        }

        public async Task<Result<EmployeeDTO>> UpdateEmployee(Guid id, EmployeeDTOw employeeModel)
        {
            var employee = await employeeUoW.Repository.ReadSingle(id);
            if (employee != null)
            {
                employee.Id = id;
                employee.FirstName = employeeModel.FirstName;
                employee.LastName = employeeModel.LastName;
                employee.Gender = employeeModel.Gender;
                employee.Salary = employeeModel.Salary;

                employeeUoW.Repository.Update(employee);
                await employeeUoW.SaveChangesAsync();

                var employeeDTO = Map.Employees(new List<Employee> { employee }).FirstOrDefault();
                return Result.Success(employeeDTO, "Updated Successfully.");
            }

            return Result.Failure<EmployeeDTO>("Employee Not Found.");
        }

        public async Task<Result<List<EmployeeDTO>>> Search(string name, Gender? gender)
        {
            try
            {
                if (string.IsNullOrEmpty(name)) return Result.Failure<List<EmployeeDTO>>("Name cannot be empty.");

                var employeeList = await employeeUoW.Repository.ReadAll();

                var employees = employeeList.Where(e => e.FirstName.Contains(name, StringComparison.OrdinalIgnoreCase) || e.FirstName.Contains(name, StringComparison.OrdinalIgnoreCase));

                if (gender.HasValue) { employees = employees.Where(e => e.Gender == gender.Value); }

                var result = Map.Employees(employees);

                return Result.Success(result);
            }
            catch { throw; }
        }
    }
}
