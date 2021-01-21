using HateoasLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using HateoasLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace HateoasLibrary.Providers
{
    public abstract class HateoasUriProvider<TPolicy> where TPolicy : InMemoryPolicyRepository.Policy
    {
        protected readonly IHttpContextAccessor ContextAccessor;

        protected HateoasUriProvider(IHttpContextAccessor contextAccessor)
        {
            ContextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }

        protected HttpContext HttpContext => ContextAccessor.HttpContext;

        public abstract (string Method, string Uri) GenerateEndpoint(TPolicy policy, object result);


    }
}
