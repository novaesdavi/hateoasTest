using HateoasLibrary;
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

        [HttpGet("{id_wheather}")]
        public IActionResult Get([FromQuery] WeatherForecastRequest request, [Hateoas][FromRoute] WeatherForecastRoute route)
        {

           var response = _getWeatherUseCase.GetWeatherExecute(request);

            request.Summary = "testeHateoas";

            return Ok(response);
            
        }
    }
}
