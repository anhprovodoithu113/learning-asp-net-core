using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SampleAPI.Repositories;
using System;
using System.Threading.Tasks;

namespace SampleAPI.Filters
{
    public class OrderExistAttribute : TypeFilterAttribute
    {
        public OrderExistAttribute() : base(typeof(OrderExistFilterImpl))
        {
        }

        private class OrderExistFilterImpl : IAsyncActionFilter
        {
            private readonly IOrderRepository _orderRepository;
            public OrderExistFilterImpl(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (!context.ActionArguments.ContainsKey("id"))
                {
                    context.Result = new BadRequestResult();
                    return;
                }

                if(!(context.ActionArguments["id"] is Guid id))
                {
                    context.Result = new BadRequestResult();
                    return;
                }

                var result = _orderRepository.Get(id);

                if(result == null)
                {
                    context.Result = new NotFoundObjectResult(new { Message = $"Item with id {id} not exist." });
                    return;
                }

                await next();
            }
        }
    }
}
