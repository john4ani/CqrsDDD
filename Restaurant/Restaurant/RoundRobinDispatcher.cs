using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Models;

namespace Restaurant
{
    public class RoundRobinDispatcher : IHandleOrder
    {
        private readonly Queue<IHandleOrder> _orderHandlers = new Queue<IHandleOrder>();

        public RoundRobinDispatcher(IEnumerable<IHandleOrder> orderHandlers)
        {
            foreach (var handler in orderHandlers)
            {
                _orderHandlers.Enqueue(handler);
            }
        }
        public void Handle(OrderDocument order)
        {
            var cook = _orderHandlers.Dequeue();
            cook.Handle(order);
            _orderHandlers.Enqueue(cook);
        }
    }
}
