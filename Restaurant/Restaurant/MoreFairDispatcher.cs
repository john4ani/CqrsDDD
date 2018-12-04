using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Models;

namespace Restaurant
{
    public class MoreFairDispatcher<T> : IHandle<T>
        where T : Message
    {
        private readonly Queue<IHandleOrderWithQueue<T>> _orderHandlers = new Queue<IHandleOrderWithQueue<T>>();

        public MoreFairDispatcher(IEnumerable<IHandle<T>> orderHandlers)
        {
            foreach (var handler in orderHandlers)
            {
                _orderHandlers.Enqueue(handler as IHandleOrderWithQueue<T>);
            }
        }
        public void Handle(T order)
        {
            var stop = false;
            while (_orderHandlers.Count > 0)
            {
                var cook = _orderHandlers.Dequeue();
                if (cook.QueueDeep < 5)
                {
                    cook.Handle(order);
                    stop = true;
                }
                _orderHandlers.Enqueue(cook);
                if(stop) break;
            }
        }

        
    }
}
