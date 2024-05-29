using EmployeeService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [Route("api/connect/[action]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        private readonly IConnectionService connectionsService;

        public ConnectionController(IConnectionService connectionsService)
        {
            this.connectionsService = connectionsService;
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetEmployeeConnectionList(int id)
        //{
        //    var result = await connectionsService.GetEmployeeConnectionList(id);
        //    if (result == null)
        //    {
        //        return NotFound(result);
        //    }
        //    return Ok(result);
        //}

        [HttpGet]
        public async Task<IActionResult> EmployeeConnections([FromRoute] Guid employeeId)
        {
            var result = await connectionsService.ConnectionList(employeeId);
            if (result.Succeeded) return Ok(result.Message);

            return BadRequest(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeToConnection([FromRoute] Guid employeeId, [FromRoute] Guid friendId)
        {
            var result = await connectionsService.AddToConnection(employeeId, friendId);
            return Ok(result);
        }

        [HttpDelete("{employeeId}, {connectionId}")]
        public async Task<IActionResult> DeleteConnection([FromRoute] Guid employeeId, [FromRoute] Guid connectionId)
        {
            var result = await connectionsService.RemoveFromConnection(employeeId, connectionId);
            if (result.Succeeded) return Ok(result.Message);

            return BadRequest(result.Message);
        }
    }
}
