using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.CustomRouting
{
    public class CurrencyConstraint : IRouteConstraint
    {
        private static readonly IList<string> _currencies = new List<string> { "EUR", "USD", "GBP" };
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return _currencies.Contains(values[routeKey]?.ToString().ToLowerInvariant());
        }
    }
}
