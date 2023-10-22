using EmployeeService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [Route("api/connect/[action]")]
    [ApiController]
    public class ConnectController : ControllerBase
    {
        private readonly IConnectionsLogicLayer connectionsLogic;

        public ConnectController(IConnectionsLogicLayer connectionsLogic)
        {
            this.connectionsLogic = connectionsLogic;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetEmployeeConnectionList(int id)
        {
            var result = await connectionsLogic.GetEmployeeConnectionList(id);
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
    }
}
