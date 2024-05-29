using EmployeeService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [Route("api/connectionrequest/[action]")]
    [ApiController]
    public class ConnectionRequestController : ControllerBase
    {
        private readonly IConnectionRequestService connectionRequestservice;

        public ConnectionRequestController(IConnectionRequestService connectionRequestservice)
        {
            this.connectionRequestservice = connectionRequestservice;
        }

        //[HttpGet("{Id}")]
        //public async Task<IActionResult> GetEmployeeConnectionRequest(int Id)
        //{
        //    var list = await connectionRequestservice.GetConnectionRequestList(Id);
        //    if (list == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(list);
        //}

        [HttpGet("{receiverId}, {senderId}")]
        public async Task<IActionResult> SendConnectionRequest([FromRoute] Guid receiverId, [FromRoute] Guid senderId)
        {
            var result = await connectionRequestservice.SendConnectionRequest(receiverId, senderId);
            if (result.Succeeded) return Ok(result.Message);

            return BadRequest(result.Message);
        }

        //[HttpDelete("{employeeId}, {requestId}")]
        //public async Task<IActionResult> RemoveConnectionRequest(int employeeId, int requestId)
        //{
        //    var result = await connectionRequestLogic.RemoveConnectionRequest(employeeId, requestId);
        //    return Ok(result);
        //}
    }
}
