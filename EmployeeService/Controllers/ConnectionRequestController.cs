using EmployeeService.DTO.Read;
using EmployeeService.Helpers;
using EmployeeService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [Route("api/connectionrequest/")]
    [ApiController]
    public class ConnectionRequestController : ControllerBase
    {
        private readonly IConnectionRequestService connectionRequestservice;

        public ConnectionRequestController(IConnectionRequestService connectionRequestservice)
        {
            this.connectionRequestservice = connectionRequestservice;
        }

        [HttpGet]
        [Route("employee/getConnectionRequests/{employeeId}")]
        public async Task<IActionResult> GetConnectionRequest([FromRoute] Guid employeeId)
        {
            try
            {
                var result = await connectionRequestservice.GetConnectionRequests(employeeId);

                if (result.Succeeded) return Ok(new JsonMessage<RequestDTO>()
                {
                    Status = true,
                    Results = result.Data
                });

                return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    Message = ex.InnerException.Message
                });

                return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("employee/sendConnectionRequest/{receiverId}/{senderId}")]
        public async Task<IActionResult> SendConnectionRequest([FromRoute] Guid receiverId, [FromRoute] Guid senderId)
        {
            try
            {
                var result = await connectionRequestservice.SendConnectionRequest(receiverId, senderId);

                if (result.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = true,
                    Message = result.Message
                });

                return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    Message = ex.InnerException.Message
                });

                return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("employee/cancelConnectionRequest/{senderId}/{receiverId}")]
        public async Task<IActionResult> CancelConnectionRequest([FromRoute] Guid senderId, [FromRoute] Guid receiverId)
        {
            try
            {
                var result = await connectionRequestservice.RemoveConnectionRequest(senderId, receiverId);

                if (result.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = true,
                    Message = result.Message
                });

                return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    Message = result.Message
                });
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    Message = ex.InnerException.Message
                });

                return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    Message = ex.Message
                });
            }
        }
    }
}
