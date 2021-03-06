using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Demo
{
    public static class SampleMiddlewareExtensions
    {
        public static IApplicationBuilder UseSampleMiddleware(this IApplicationBuilder builder)
        {

            #region example1
            // The SampleMiddleware will only be added to our pipeline if it is called as a URI
            // with the specified path.

            //return builder.Map("/test/path", _ => _.UseMiddleware<SampleMiddleware>());
            #endregion


            #region example2

            // If the request is HTTPS, we will innitialize the SampleMiddleware class
            return builder.MapWhen(context => context.Request.IsHttps, _ => _.UseMiddleware<SampleMiddleware>());
            #endregion
        }
    }
}
