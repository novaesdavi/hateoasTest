using HateoasLibrary.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HateoasLibrary.Providers
{
    internal class HateoasExternalUriProvider : HateoasUriProvider<InMemoryPolicyRepository.ExternalPolicy>
    {
        public HateoasExternalUriProvider(IHttpContextAccessor contextAccessor)
            : base(contextAccessor)
        { }


        public override (string Method, string Uri) GenerateEndpoint(InMemoryPolicyRepository.ExternalPolicy policy, object result)
        {
            return (policy.Method, string.Join(", ", result.ToString()));
        }

    }
}
