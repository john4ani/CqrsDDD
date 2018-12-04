using System;
using Restaurant.Events;
using Restaurant.Models;

namespace Restaurant.ActorsForTopicPubSub
{
    public class Waiter
    {
        private readonly TopicBasedPubSub _orderHandler;

        private readonly Random _rnd = new Random();

        public Waiter(TopicBasedPubSub orderHandler)
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
            _orderHandler.Publish(new OrderPlaced { Order = newOrder });
        }
    }
}
