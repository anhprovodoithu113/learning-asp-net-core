using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SampleAPI.Models;
using SampleAPI.Repositories;
using SampleAPI.Requests;
using SampleAPI.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : Controller
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

        //[HttpPut("{id:guid}")]
        [HttpPatch("{id:guid}")]
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
