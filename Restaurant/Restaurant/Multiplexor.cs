using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Models;

namespace Restaurant
{
    public class Multiplexor : IHandleOrder
    {
        private readonly IEnumerable<IHandleOrder> _orderHandlers;

        public Multiplexor(IEnumerable<IHandleOrder> orderHandlers)
        {
            _orderHandlers = orderHandlers;
        }
        public void Handle(OrderDocument order)
        {
            foreach (var handler in _orderHandlers)
            {
                handler.Handle(order);
            }
        }
    }
}
