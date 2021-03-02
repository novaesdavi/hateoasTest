using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.UseCase;
using HateoasLibrary.Extensions;
using System.Net.Http;

namespace WebApplication3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IGetWeatherUseCase, GetWeatherUseCase>();

            services.AddLink(builder =>
            {
                builder.AddPolicy<WeatherForecastResponse, WeatherForecastRequest>(model =>
                {
                    //model.AddExternalUri("chamada_principal", HttpMethod.Get.ToString(), (res,req) => $"/WeatherForecast/{res.Weathers.Count()}/teste/{req.Summary}", m => m.Response.Weathers.Count() >= 0, m => m.Response.Weathers.Count() >= 0);
                    model.AddExternalUri("chamada_principal_res_req", HttpMethod.Get.ToString(), (res, req) => $"/WeatherForecast/{res.Weathers.Count()}/teste/{req.Summary}");
                });
                builder.AddPolicy<WeatherForecastResponse>(model =>
                {
                    model.AddExternalUri("chamada_principal", HttpMethod.Get.ToString(), m => $"/teste", m => m.Response.Weathers.Count() >= 0);
                    model.AddExternalUri("chamada_secundaria", HttpMethod.Get.ToString(), m => $"/WeatherForecast");
                }, c => c.IncludeCondition(model => model.Response.Weathers.Any()));
            }

            ); ;

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
