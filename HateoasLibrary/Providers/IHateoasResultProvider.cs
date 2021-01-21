using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HateoasLibrary.Providers
{
    public interface IHateoasResultProvider
    {
        bool HasAnyPolicy(IActionResult actionResult, out ObjectResult objectResult);

        Task<IActionResult> GetContentResultAsync(ObjectResult result);
    }
}
