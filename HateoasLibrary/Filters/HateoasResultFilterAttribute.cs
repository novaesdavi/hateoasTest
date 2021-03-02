using HateoasLibrary.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HateoasLibrary.Filters
{
    public class HateoasResultFilterAttribute : IAsyncActionFilter
    {

        private readonly IHateoasResultProvider _resultProvider;

        public HateoasResultFilterAttribute(IHateoasResultProvider resultProvider)
        {
            _resultProvider = resultProvider ?? throw new ArgumentNullException(nameof(resultProvider));
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultAction = await next();

            if (_resultProvider.HasAnyValidCondition(resultAction.Result, out ObjectResult result))
            {
                var finalResult = await _resultProvider.GetContentResultAsync(result, context).ConfigureAwait(false);
                if (finalResult != null)
                {
                    await finalResult.ExecuteResultAsync(context).ConfigureAwait(false);
                }
            }
        }
    }
}
