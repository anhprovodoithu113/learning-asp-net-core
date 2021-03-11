using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SampleAPI.Filters;
using SampleAPI.Models;
using SampleAPI.Repositories;
using SampleAPI.Requests;
using SampleAPI.Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleAPI.Controllers
{
    [Route("api/order")]
    [ApiController]
    //[ServiceFilter(typeof(CustomActionFilter))]
    [CustomActionFilterAttribute]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Map(_orderRepository.Get()));
        }

        [HttpGet("{id:guid}")]
        [OrderExist]
        public IActionResult GetById(Guid id)
        {
            return Ok(_orderRepository.Get(id));
        }

        [HttpPost]
        public IActionResult Post(OrderRequest request)
        {
            var order = new Order()
            {
                Id = Guid.NewGuid(),
                ItemsIds = request.ItemsIds
            };

            _orderRepository.Add(order);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, null);
        }

        [HttpPut("{id:guid}")]
        [OrderExist]
        public IActionResult Put(Guid id, OrderRequest request)
        {
            if(request.ItemsIds == null)
            {
                return BadRequest();
            }

            var order = _orderRepository.Get(id);
            if(order == null)
            {
                return NotFound(new { Message = $"Item with id {id} not exist." });
            }

            order = Map(request, order);
            _orderRepository.Update(id, order);
            return Ok();
        }

        [HttpPatch("{id:guid}")]
        [OrderExist]
        public IActionResult Patch(Guid id, JsonPatchDocument<Order> requestOp)
        {
            var order = _orderRepository.Get(id);
            if (order == null)
                return NotFound(new { Message = $"Item with the id {id} not exist." });

            requestOp.ApplyTo(order);
            _orderRepository.Update(id, order);
            return Ok();
        }

        [HttpDelete]
        [OrderExist]
        public IActionResult Delete(Guid id)
        {
            var order = _orderRepository.Get(id);
            if (order == null)
            {
                return NotFound(new { Message = $"Item with id {id} not exist." });
            }
            _orderRepository.Delete(id);
            return Ok();
        }

        private Order Map(OrderRequest request, Order order)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<OrderResponse> Map(IEnumerable<Order> orders)
        {
            return orders.Select(Map).ToList();
        }

        private Order Map(OrderRequest request)
        {
            return new Order
            {
                Id = Guid.NewGuid(),
                ItemsIds = request.ItemsIds,
                Currency = request.Currency
            };
        }

        private OrderResponse Map(Order order)
        {
            return new OrderResponse
            {
                Id = order.Id,
                ItemsIds = order.ItemsIds,
                Currency = order.Currency
            };
        }
    }
}
