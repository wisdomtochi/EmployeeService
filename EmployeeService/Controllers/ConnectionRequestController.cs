using EmployeeService.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionRequestController : ControllerBase
    {
        private readonly ConnectionRequestLogicLayer connectionRequestLogic;

        public ConnectionRequestController(ConnectionRequestLogicLayer connectionRequestLogic)
        {
            this.connectionRequestLogic = connectionRequestLogic;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllEmployeeConnectionRequest(int id)
        {
            var list = await connectionRequestLogic.GetConnectionRequestList(id);
            return Ok(list);
        }

        [HttpGet("{employeeId}, {requestId}")]
        public async Task<IActionResult> SendConnectionRequest(int employeeId, int requestId)
        {
            await connectionRequestLogic.SendConnectionRequest(employeeId, requestId);
            return Ok();
        }
    }
}
