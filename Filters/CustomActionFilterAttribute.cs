using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Filters
{
    public class CustomActionFilterAttribute : TypeFilterAttribute
    {
        public CustomActionFilterAttribute() : base(typeof(CustomActionFilter))
        {

        }
    }
}
