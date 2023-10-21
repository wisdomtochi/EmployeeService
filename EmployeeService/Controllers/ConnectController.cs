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
        public async Task<ActionResult> GetEmployeeConnectionList(int Id)
        {
            var result = await connectionsLogic.GetEmployeeConnectionList(Id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> AllConnections()
        {
            var result = await connectionsLogic.ConnectionList();
            return Ok(result);
        }
    }
}
