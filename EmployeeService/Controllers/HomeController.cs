using EmployeeService.Domains;
using EmployeeService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [Route("api/home/[action]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService employeeRepository;

        public EmployeesController(IEmployeeService employeeRepository, IConnectionService connectionsLogic)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee([FromHeader] int id)
        {
            var emp = await employeeRepository.GetEmployee(id);
            return Ok(emp);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var emp = await employeeRepository.GetAllEmployee();
            return Ok(emp);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
        {
            var result = await employeeRepository.CreateEmployee(employee);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee([FromRoute] Guid Id, [FromBody] Employee employee)
        {
            var emp = employeeRepository.UpdateEmployee(Id, employee);
            return Ok(emp);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var result = employeeRepository.DeleteEmployee(id);
            return Ok(result);
        }
    }
}
