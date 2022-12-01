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

        [HttpGet("get-all-actions")]
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

        [HttpPost("leave-the-room")]
        public IActionResult LeaveTheRoom([FromBody] LeaveTheRoomRequest request)
        {
            _application.LeaveTheRoom(request);
            return Ok();
        }

        [HttpPost("comment")]
        public IActionResult Comment([FromBody] CommentRequest request)
        {
            _application.Comment(request);
            return Ok();
        }

        [HttpPost("high-five")]
        public IActionResult HighFive([FromBody] HighFiveRequest request)
        {
            _application.HighFive(request);
            return Ok();
        }

    }
}