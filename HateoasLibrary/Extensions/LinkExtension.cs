using HateoasLibrary.Filters;
using HateoasLibrary.Providers;
using HateoasLibrary.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HateoasLibrary.Extensions
{
    public static class LinkExtension
    {

        private static readonly LinkBuilder linkBuilder = new LinkBuilder();

        public static IServiceCollection AddLink(this IServiceCollection services, Action<LinkBuilder> configure = null)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<HateoasUriProvider<InMemoryPolicyRepository.ExternalPolicy>, HateoasExternalUriProvider>();
            
            services.AddTransient<IHateoasResultProvider, HateoasResultProvider>();

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add<HateoasResultFilterAttribute>();
            });

            configure?.Invoke(linkBuilder);

            return services;
        }
    }
}
