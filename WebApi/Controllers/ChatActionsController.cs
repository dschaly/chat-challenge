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

        /// <summary>
        /// Lists every action registered
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all-actions")]
        public IActionResult GetAllActions()
        {
            var response = _application.GetAllActions();
            return Ok(response);
        }

        /// <summary>
        /// Lists every user registered
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all-user")]
        public IActionResult GetAllUsers()
        {
            var response = _application.GetAllUsers();
            return Ok(response);
        }

        /// <summary>
        /// Register user entering the chat room
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("enter-the-room")]
        public IActionResult EnterTheRoom([FromBody] EnterTheRoomRequest request)
        {
            _application.EnterTheRoom(request);
            return Ok();
        }

        /// <summary>
        /// Register user leaving the chat room
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("leave-the-room")]
        public IActionResult LeaveTheRoom([FromBody] LeaveTheRoomRequest request)
        {
            _application.LeaveTheRoom(request);
            return Ok();
        }

        /// <summary>
        /// Register one user comment
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("comment")]
        public IActionResult Comment([FromBody] CommentRequest request)
        {
            _application.Comment(request);
            return Ok();
        }

        /// <summary>
        /// Register a user's high-five to another user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("high-five")]
        public IActionResult HighFive([FromBody] HighFiveRequest request)
        {
            _application.HighFive(request);
            return Ok();
        }

        /// <summary>
        /// Display history, by minute, on a date interval
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("get-history-by-minute")]
        public IActionResult GetHistoryByMinute([FromQuery] RoomActionsByMinuteFilter filter)
        {
            var response = _application.GetHistoryByMinute(filter);
            return Ok(response);
        }

        /// <summary>
        /// Display history, by hour, on a date interval
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("get-history-by-hour")]
        public IActionResult GetHistoryByHour([FromQuery] RoomActionsByHourFilter filter)
        {
            var response = _application.GetHistoryByHour(filter);
            return Ok(response);
        }
    }
}