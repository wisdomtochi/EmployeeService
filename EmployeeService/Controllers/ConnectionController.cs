using EmployeeService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [Route("api/connect/[action]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        private readonly IConnectionsLogicLayer connectionsLogic;

        public ConnectionController(IConnectionsLogicLayer connectionsLogic)
        {
            this.connectionsLogic = connectionsLogic;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeConnectionList(int id)
        {
            var result = await connectionsLogic.GetEmployeeConnectionList(id);
            if (result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> AllConnections()
        {
            var result = await connectionsLogic.ConnectionList();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeToConnection(int employeeId, int connectionId)
        {
            var result = await connectionsLogic.AddToConnection(employeeId, connectionId);
            return Ok(result);
        }

        [HttpDelete("{employeeId}, {connectionId}")]
        public async Task<IActionResult> DeleteConnection(int employeeId, int connectionId)
        {
            var result = await connectionsLogic.DeleteFromConnection(employeeId, connectionId);
            return Ok(result);
        }
    }
}
