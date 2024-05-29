using EmployeeService.DTO.Read;
using EmployeeService.DTO.Write;
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
        public async Task<IActionResult> GetEmployee([FromHeader] Guid id)
        {
            var result = await employeeService.GetEmployee(id);
            if (result.Succeeded) return Ok(new JsonMessage<EmployeeDTO>()
            {
                Status = true,
                Results = new List<EmployeeDTO> { result.Data }
            });

            return BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var result = await employeeService.GetAllEmployee();
            if (result.Succeeded) return Ok(result.Message);


            return BadRequest(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDTOw employee)
        {
            try
            {
                var result = await employeeService.CreateEmployee(employee);
                if (result.Succeeded) return Ok(result.Message);
                //return Ok(result.);
                return BadRequest(result.Message);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid Id, [FromBody] EmployeeDTOw employee)
        {
            var result = await employeeService.UpdateEmployee(Id, employee);
            if (result.Succeeded) return Ok(result.Message);

            return BadRequest(result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var result = await employeeService.DeleteEmployee(id);
            if (result.Succeeded) return Ok(result.Message);

            return BadRequest(result.Message);
        }
    }
}
