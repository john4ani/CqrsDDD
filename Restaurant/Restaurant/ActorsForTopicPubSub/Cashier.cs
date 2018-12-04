using System;
using System.Collections.Generic;
using System.Linq;
using Restaurant.Commands;
using Restaurant.Events;
using Restaurant.Models;

namespace Restaurant.ActorsForTopicPubSub
{
    public class Cashier : IHandle<TakePayment>
    {
        private readonly TopicBasedPubSub _hadler;
        private readonly Dictionary<Guid, OrderDocument> _orders = new Dictionary<Guid, OrderDocument>();

        public Cashier(TopicBasedPubSub hadler)
        {
            _hadler = hadler;
        }

        public void Handle(TakePayment order)
        {
            if(!_orders.ContainsKey(order.Order.Id))
                _orders.Add(order.Order.Id,order.Order);
        }

        public void Pay(Guid id)
        {
            OrderDocument order;
            if (!_orders.TryGetValue(id, out order)) return;
            order.IsPaid = true;
            _orders.Remove(id);
            _hadler.Publish(new OrderPaid {Order = order } );
        }

        public List<OrderDocument> UnpaiedOrders => _orders.Values.Where(o=>!o.IsPaid).ToList();
    }
}
