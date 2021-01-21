using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.UseCase
{
    public class GetWeatherUseCase : IGetWeatherUseCase
    {
        private static readonly string[] Summaries = new[]
{
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecastResponse GetWeatherExecute(WeatherForecastRequest request)
        {
            var rng = new Random();

            var listatempo = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray();

            //ValidaLista<WeatherForecast> validaLista = new ValidaLista<WeatherForecast>();
            var wheather = new WeatherForecastResponse();

            wheather.Weathers = listatempo;
            //listaComUmItem.Add(validaLista.ObtemPrimeirItemLista<WeatherForecastResponse>(listatempo, request.Summary));

            return wheather;
        }

    }

    //public class ValidaLista <TLista> where TLista : WeatherBase
    //{
    //    public TOutput ObtemPrimeirItemLista<TOutput>(IEnumerable<TLista> lista, string filtro) where TOutput :  WeatherBase, new()
    //    {
    //        TOutput output = new TOutput();

    //        var listafiltrada = lista.Where(w => w.Date > DateTime.Now).First();

    //        output.Date = listafiltrada.Date;

    //        return output;
    //    }
    //}
}
