using System;
using Restaurant.Events;
using Restaurant.Models;

namespace Restaurant
{
    public class OrderPrinter : IHandle<OrderPaid>
    {
        private readonly TopicBasedPubSub _bus;

        public OrderPrinter(TopicBasedPubSub bus)
        {
            _bus = bus;
            _bus.Subscribe(this);
        }

        public void Handle(OrderPaid order)
        {
            Console.WriteLine(order.Order.Serialize()); 
        }
    }
}
