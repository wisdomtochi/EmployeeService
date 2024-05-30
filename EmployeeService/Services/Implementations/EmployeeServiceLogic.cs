﻿using EmployeeService.DataAccess.Interfaces;
using EmployeeService.Domains;
using EmployeeService.DTO.Read;
using EmployeeService.DTO.Write;
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

            List<EmployeeDTO> employees = Map.Employees(employeeList.ToList());

            return Result.Success(employees);
        }

        public async Task<Result<EmployeeDTO>> GetEmployee(Guid id)
        {
            var EmployeeExist = await employeeUoW.Repository.ReadSingle(id);

            if (EmployeeExist == null) return Result.Failure<EmployeeDTO>("Employee Not Found.");

            var employee = Map.Employees(new List<Employee> { EmployeeExist }).FirstOrDefault();

            return Result.Success(employee);
        }

        public async Task<Result> CreateEmployee(EmployeeDTOw employee)
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
                return Result.Success("Employee Created Successfully.");
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
                employee.Id = id;
                employee.FirstName = employeeModel.FirstName;
                employee.LastName = employeeModel.LastName;
                employee.Gender = employeeModel.Gender;
                employee.Salary = employeeModel.Salary;

                employeeUoW.Repository.Update(employee);
                await employeeUoW.SaveChangesAsync();
                return Result.Success("Updated Successfully.");
            }
            return Result.Failure("Employee Not Found.");
        }
    }
}
