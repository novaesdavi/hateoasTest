using HateoasLibrary.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HateoasLibrary.Filters
{
    public class HateoasResultFilterAttribute : ResultFilterAttribute
    {
        private readonly IHateoasResultProvider _resultProvider;

        public HateoasResultFilterAttribute(IHateoasResultProvider resultProvider)
        {
            _resultProvider = resultProvider ?? throw new ArgumentNullException(nameof(resultProvider));
        }
     
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (_resultProvider.HasAnyValidCondition(context.Result, out ObjectResult result))
            {
                var finalResult = await _resultProvider.GetContentResultAsync(result, context.ActionDescriptor.Parameters).ConfigureAwait(false);
                if (finalResult != null)
                {
                    context.Result = finalResult;
                }
            }

            await base.OnResultExecutionAsync(context, next).ConfigureAwait(false);
        }
    }
}
