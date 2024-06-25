using EmployeeService.DTO.Read;
using EmployeeService.Helpers;
using EmployeeService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [Route("api/connect/")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        private readonly IConnectionService connectionsService;

        public ConnectionController(IConnectionService connectionsService)
        {
            this.connectionsService = connectionsService;
        }

        [HttpGet]
        [Route("employee/getConnectionList/{employeeId}")]
        public async Task<IActionResult> EmployeeConnections([FromRoute] Guid employeeId)
        {
            try
            {
                var result = await connectionsService.ConnectionList(employeeId);
                if (result.Succeeded) return Ok(new JsonMessage<ConnectionDTO>()
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
                return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("employee/addToConnection/{employeeId}/{friendId}")]
        public async Task<IActionResult> AddEmployeeToConnection([FromRoute] Guid employeeId, [FromRoute] Guid friendId)
        {
            try
            {
                var result = await connectionsService.AddToConnection(employeeId, friendId);
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

        [HttpDelete]
        [Route("employee/removeFromConnection/{employeeId}/{connectionId}")]
        public async Task<IActionResult> DeleteConnection([FromRoute] Guid employeeId, [FromRoute] Guid connectionId)
        {
            try
            {
                var result = await connectionsService.RemoveFromConnection(employeeId, connectionId);
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
                return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    Message = ex.Message
                });
            }
        }
    }
}
