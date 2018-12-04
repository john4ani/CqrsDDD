using System;
using System.Collections.Generic;
using System.Linq;
using Restaurant.Models;

namespace Restaurant.Actors
{
    public class Cashier : IHandleOrder
    {
        private readonly IHandleOrder _hadler;
        private readonly Dictionary<Guid, OrderDocument> _orders = new Dictionary<Guid, OrderDocument>();

        public Cashier(IHandleOrder hadler)
        {
            _hadler = hadler;
        }

        public void Handle(OrderDocument order)
        {
            if(!_orders.ContainsKey(order.Id))
                _orders.Add(order.Id,order);
        }

        public void Pay(Guid id)
        {
            OrderDocument order;
            if (!_orders.TryGetValue(id, out order)) return;
            order.IsPaid = true;
            _orders.Remove(id);
            _hadler.Handle(order);
        }

        public List<OrderDocument> UnpaiedOrders => _orders.Values.Where(o=>!o.IsPaid).ToList();
    }
}
