using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.UseCase;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {


        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IGetWeatherUseCase _getWeatherUseCase;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IGetWeatherUseCase getWeatherUseCase)
        {
            _logger = logger;
            _getWeatherUseCase = getWeatherUseCase;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] WeatherForecastRequest request)
        {

           var response = _getWeatherUseCase.GetWeatherExecute(request);
            return Ok(response);
            
        }
    }
}
