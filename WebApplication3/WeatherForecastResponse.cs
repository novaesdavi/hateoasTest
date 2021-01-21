using System;
using System.Collections.Generic;

namespace WebApplication3
{
    public class WeatherForecastResponse
    {
        public IEnumerable<WeatherForecast> Weathers { get; set; }
    }
}