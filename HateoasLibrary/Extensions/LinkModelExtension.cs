using System;
using System.Collections.Generic;
using System.Text;

namespace HateoasLibrary.Extensions
{
    public class LinkModelExtension<TResponse> where TResponse : class
    {
        public TResponse Data { get; set; }

    }
}
