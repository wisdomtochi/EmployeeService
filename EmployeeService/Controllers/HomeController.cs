using EmployeeService.DTO.Read;
using EmployeeService.DTO.Write;
using EmployeeService.Enums;
using EmployeeService.Helpers;
using EmployeeService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [Route("api/home/")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeServiceLogic employeeService;

        public EmployeesController(IEmployeeServiceLogic employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet]
        [Route("employees/GetEmployee/{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            try
            {
                var result = await employeeService.GetEmployee(id);
                if (result.Succeeded) return Ok(new JsonMessage<EmployeeDTO>()
                {
                    Status = true,
                    Result = result.Data
                });

                return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("allEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var result = await employeeService.GetAllEmployee();
                if (result.Succeeded) return Ok(new JsonMessage<EmployeeDTO>()
                {
                    Status = true,
                    Results = result.Data,
                    Message = result.Message
                });

                return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("employees/search")]
        public async Task<IActionResult> Search(string name, Gender? gender)
        {
            try
            {
                var result = await employeeService.Search(name, gender);

                if (result.Data.Any()) return Ok(new JsonMessage<EmployeeDTO>()
                {
                    Status = true,
                    Results = result.Data
                });

                return NotFound();
            }
            catch (Exception ex)
            {
                return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("employees/createemployee")]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDTOw employee)
        {
            try
            {
                var result = await employeeService.CreateEmployee(employee);
                if (result.Succeeded) return Ok(new JsonMessage<EmployeeDTO>()
                {
                    Status = true,
                    Message = result.Message
                });

                return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPut]
        [Route("employees/updateEmployee/{id}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, [FromBody] EmployeeDTOw employee)
        {
            try
            {
                var result = await employeeService.UpdateEmployee(id, employee);
                if (result.Succeeded) return Ok(new JsonMessage<EmployeeDTO>()
                {
                    Status = true,
                    Message = result.Message
                });

                return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    Message = ex.Message
                });
            }
        }

        [HttpDelete]
        [Route("employees/deleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            try
            {
                var result = await employeeService.DeleteEmployee(id);
                if (result.Succeeded) return Ok(new JsonMessage<EmployeeDTO>()
                {
                    Status = true,
                    Message = result.Message
                });

                return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    Message = ex.Message
                });
            }
        }
    }
}
