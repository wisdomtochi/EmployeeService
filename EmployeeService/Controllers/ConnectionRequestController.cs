using EmployeeService.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [Route("api/connectionrequest/[action]")]
    [ApiController]
    public class ConnectionRequestController : ControllerBase
    {
        private readonly ConnectionRequestLogicLayer connectionRequestLogic;

        public ConnectionRequestController(ConnectionRequestLogicLayer connectionRequestLogic)
        {
            this.connectionRequestLogic = connectionRequestLogic;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetEmployeeConnectionRequest(int Id)
        {
            var list = await connectionRequestLogic.GetConnectionRequestList(Id);
            return Ok(list);
        }

        [HttpGet("{receiverId}, {senderId}")]
        public async Task<IActionResult> SendConnectionRequest(int receiverId, int senderId)
        {
            await connectionRequestLogic.SendConnectionRequest(receiverId, senderId);
            return Ok();
        }
    }
}
