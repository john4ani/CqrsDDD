using System;
using System.Threading;
using Restaurant.Models;

namespace Restaurant.Actors
{
    public class Cook : IHandleOrder
    {
        private readonly IHandleOrder _orderHandler;
        private readonly string _name;
        private object _lock = new object();

        public Cook(IHandleOrder orderHandler,string name)
        {
            _orderHandler = orderHandler;
            _name = name;
        }
        public void Handle(OrderDocument order)
        {
            lock (_lock)
            {
                var r = new Random();
                var sleep = r.Next(1000,2000);
                Thread.Sleep(sleep);
                order.AddIngredient("salt");
                Console.Write($"Cooker {_name}:");
                _orderHandler.Handle(order);
            }
        }
    }
}
