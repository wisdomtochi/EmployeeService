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

        [HttpGet("{senderId}, {receiverId}")]
        public async Task<IActionResult> SendConnectionRequest(int senderId, int receiverId)
        {
            await connectionRequestLogic.SendConnectionRequest(receiverId, senderId);
            return Ok();
        }
    }
}
