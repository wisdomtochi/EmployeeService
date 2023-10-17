using EmployeeService.Data_Access;
using EmployeeService.Domains;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers.Home
{
    [Route("api/home/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var emp = await employeeRepository.GetEmployee(id);
            return Ok(emp);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var emp = await employeeRepository.GetAllEmployee();
            return Ok(emp);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Employee employee)
        {
            var newEmployee = await employeeRepository.CreateEmployee(employee);
            return Ok(newEmployee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Employee employeeModel)
        {
            await employeeRepository.UpdateEmployee(employeeModel);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await employeeRepository.DeleteEmployee(id);
            return Ok();
        }
    }
}
