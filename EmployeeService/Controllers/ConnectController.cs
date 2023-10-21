using EmployeeService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConnectController : ControllerBase
    {
        private readonly IConnectionsLogicLayer connectionsLogic;

        public ConnectController(IConnectionsLogicLayer connectionsLogic)
        {
            this.connectionsLogic = connectionsLogic;
        }

        //[HttpGet]
        //public IActionResult GetConnections()
        //{
        //    var result = connectionsLogic.ConnectionList();
        //    return Ok(result);
        //}
    }
}
