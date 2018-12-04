using System;
using Restaurant.Models;

namespace Restaurant.Actors
{
    public class Waiter
    {
        private readonly IHandleOrder _orderHandler;

        private readonly Random _rnd = new Random();

        public Waiter(IHandleOrder orderHandler)
        {
            _orderHandler = orderHandler;
        }

        public void PlaceOrder(Guid orderId)
        {
            var newOrder = new OrderDocument
            {
                Id = orderId,
                IsDodgy = _rnd.Next(0, 100) > 80
            };
            _orderHandler.Handle(newOrder);
        }
    }
}
