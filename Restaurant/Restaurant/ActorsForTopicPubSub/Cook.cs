using System;
using System.Collections.Generic;
using System.Threading;
using Restaurant.Commands;
using Restaurant.Events;
using Restaurant.Models;

namespace Restaurant.ActorsForTopicPubSub
{
    public class Cook : IHandle<CookFood>
    {
        private readonly TopicBasedPubSub _orderHandler;
        private readonly string _name;
        private readonly HashSet<Guid> _cookedOrders;
        private object _lock = new object();

        public Cook(TopicBasedPubSub orderHandler,string name, HashSet<Guid> cookedOrders)
        {
            _orderHandler = orderHandler;
            _name = name;
            _cookedOrders = cookedOrders;
        }
        public void Handle(CookFood order)
        {
            lock (_lock)
            {
                if (!_cookedOrders.Contains(order.Order.Id))
                {
                    var r = new Random();
                    var sleep = r.Next(1000, 2000);
                    Thread.Sleep(sleep);
                    order.Order.AddIngredient("salt");
                    Console.Write($"Cooker {_name}:");
                    _orderHandler.Publish(new FoodCoocked {Order = order.Order});
                    _cookedOrders.Add(order.Order.Id);
                }
            }
        }
    }
}
