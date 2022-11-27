using Domain.Contracts.Application;
using Domain.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatActionsController : ControllerBase
    {
        private readonly ILogger<ChatActionsController> _logger;
        private readonly IRoomActionApplication _application;

        public ChatActionsController(ILogger<ChatActionsController> logger, IRoomActionApplication application)
        {
            _logger = logger;
            _application = application;
        }

        [HttpGet("GetAllActions")]
        public IActionResult GetAllActions()
        {
            var response = _application.GetAllActions();
            return Ok(response);
        }

        [HttpPost("enter-the-room")]
        public IActionResult EnterTheRoom([FromBody] EnterTheRoomRequest request)
        {
            _application.EnterTheRoom(request);
            return Ok();
        }
    }
}