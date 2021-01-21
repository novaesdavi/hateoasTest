using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.UseCase
{
    public interface IGetWeatherUseCase
    {
        WeatherForecastResponse GetWeatherExecute(WeatherForecastRequest request);
    }
}
