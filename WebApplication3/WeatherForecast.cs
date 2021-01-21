using System;

namespace WebApplication3
{
    public class WeatherForecast : WeatherBase
    {
        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}
