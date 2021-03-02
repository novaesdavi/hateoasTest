using HateoasLibrary.Providers;
using HateoasLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HateoasLibrary.Filters
{
    //public class ControllerFilter : IAsyncActionFilter
    //{

    //    private readonly IHateoasResultProvider _resultProvider;

    //    public ControllerFilter(IHateoasResultProvider resultProvider)
    //    {
    //        _resultProvider = resultProvider ?? throw new ArgumentNullException(nameof(resultProvider));
    //    }
    //    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    //    {
    //        var resultAction = await next();

    //        if (_resultProvider.HasAnyValidCondition(resultAction.Result, out ObjectResult result))
    //        {
    //            //var listaDeRequests = InMemoryPolicyRepository.InMemoryPolicies.Where(p => )).ToList();


    //            //context.ActionArguments.Select(s => s.Value).Select(s => s.ToString()).Contains(p.TypeRequest?.FullName
                    
    //            var finalResult = await _resultProvider.GetContentResultAsync(result, context).ConfigureAwait(false);
    //            if (finalResult != null)
    //            {
    //                await finalResult.ExecuteResultAsync(context).ConfigureAwait(false);
    //            }
    //        }
    //    }
    //}
}
