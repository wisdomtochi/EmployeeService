using EmployeeService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [Route("api/connectionrequest/[action]")]
    [ApiController]
    public class ConnectionRequestController : ControllerBase
    {
        private readonly IConnectionRequestLogicLayer connectionRequestLogic;

        public ConnectionRequestController(IConnectionRequestLogicLayer connectionRequestLogic)
        {
            this.connectionRequestLogic = connectionRequestLogic;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetEmployeeConnectionRequest(int Id)
        {
            var list = await connectionRequestLogic.GetConnectionRequestList(Id);
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        [HttpGet("{receiverId}, {senderId}")]
        public async Task<IActionResult> SendConnectionRequest(int receiverId, int senderId)
        {
            await connectionRequestLogic.SendConnectionRequest(receiverId, senderId);
            return Ok();
        }

        [HttpDelete("{employeeId}, {requestId}")]
        public async Task<IActionResult> RemoveConnectionRequest(int employeeId, int requestId)
        {
            var result = await connectionRequestLogic.RemoveConnectionRequest(employeeId, requestId);
            return Ok(result);
        }
    }
}
