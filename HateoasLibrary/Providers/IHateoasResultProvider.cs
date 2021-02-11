using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HateoasLibrary.Providers
{
    public interface IHateoasResultProvider
    {
        bool HasAnyPolicy(IActionResult actionResult, IList<ParameterDescriptor> parametersRequest, out ObjectResult objectResult);
        bool HasAnyValidCondition(IActionResult actionResult, out ObjectResult objectResult);
        Task<IActionResult> GetContentResultAsync(ObjectResult result, IList<ParameterDescriptor> parametersRequest);
    }
}
