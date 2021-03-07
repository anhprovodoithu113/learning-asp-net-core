using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace SampleAPI.Filters
{
    public class CustomActionFilterAsync : IAsyncActionFilter
    {
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            throw new System.NotImplementedException();
        }
    }
}
