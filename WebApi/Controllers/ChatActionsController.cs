using Domain.Contracts.Application;
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

        //[HttpGet(Name = "GetWeatherForecast")]
        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            var result = _application.GetHistoryByMinute();
            return Ok(result);
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //})
            //.ToArray();
        }
    }
}