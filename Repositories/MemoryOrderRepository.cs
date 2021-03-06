using SampleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Repositories
{
    public class MemoryOrderRepository : IOrderRepository
    {
        private IList<Order> _orders { get; set; }
        public MemoryOrderRepository()
        {
            _orders = new List<Order>();
        }
        public void Add(Order order)
        {
            _orders.Add(order);
        }

        public Order Delete(Guid orderId)
        {
            var target = _orders.FirstOrDefault(o => o.Id == orderId);
            target.IsInactive = true;
            Update(orderId, target);

            return target;
        }

        public IEnumerable<Order> Get() => _orders.Where(m => !m.IsInactive).ToList();

        public Order Get(Guid orderId)
        {
            return _orders
                .FirstOrDefault(m => m.Id == orderId && !m.IsInactive);
        }

        public void Update(Guid orderId, Order order)
        {
            var target = _orders.FirstOrDefault(m => m.Id == orderId);
            if (target != null) target.ItemsIds = order.ItemsIds;
        }
    }
}
