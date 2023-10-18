﻿using EmployeeService.Data_Access;
using EmployeeService.Domains;
using EmployeeService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers.Home
{
    [Route("api/home/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IConnectionsLogicLayer connectionsLogic;

        public EmployeesController(IEmployeeRepository employeeRepository, IConnectionsLogicLayer connectionsLogic)
        {
            this.employeeRepository = employeeRepository;
            this.connectionsLogic = connectionsLogic;
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
            var result = await connectionsLogic.AddToConnection(employee);
            return Ok(result);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put([FromBody] Employee employeeModel)
        //{
        //    await employeeRepository.UpdateEmployee(employeeModel);
        //    return Ok();
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await employeeRepository.DeleteEmployee(id);
            return Ok();
        }
    }
}
