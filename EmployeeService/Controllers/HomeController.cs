using EmployeeService.DTO.Read;
using EmployeeService.DTO.Write;
using EmployeeService.Enums;
using EmployeeService.Helpers;
using EmployeeService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [Route("api/home/[action]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeServiceLogic employeeService;

        public EmployeesController(IEmployeeServiceLogic employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var result = await employeeService.GetEmployee(id);
            if (result.Succeeded) return Ok(new JsonMessage<EmployeeDTO>()
            {
                Status = true,
                Result = result.Data
            });

            return BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var result = await employeeService.GetAllEmployee();
            if (result.Succeeded) return Ok(new JsonMessage<EmployeeDTO>()
            {
                Status = true,
                Results = result.Data,
                Message = result.Message
            });

            return BadRequest(result.Message);
        }

        [HttpGet]
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving value from server.");
            }
        }

        [HttpPost]
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

                return BadRequest(result.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving value from server.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, [FromBody] EmployeeDTOw employee)
        {
            var result = await employeeService.UpdateEmployee(id, employee);
            if (result.Succeeded) return Ok(new JsonMessage<EmployeeDTO>()
            {
                Status = true,
                Message = result.Message
            });

            return BadRequest(result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var result = await employeeService.DeleteEmployee(id);
            if (result.Succeeded) return Ok(new JsonMessage<EmployeeDTO>()
            {
                Status = true,
                Message = result.Message
            });

            return BadRequest(result.Message);
        }
    }
}
