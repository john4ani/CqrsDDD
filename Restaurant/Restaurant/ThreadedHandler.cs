using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Restaurant.Models;

namespace Restaurant
{
    public class ThreadedHandler<T> : IHandleOrderWithQueue<T>, IStartable, IDisposable
        where T : Message
    {
        private readonly IHandle<T> _handlerOrder;
        private ConcurrentQueue<T> _queue;
        private List<Thread> _threadList;

        public ThreadedHandler(IHandle<T> handlerOrder)
        {
            _handlerOrder = handlerOrder;
            _queue = new ConcurrentQueue<T>();
            _threadList = new List<Thread>();
        }

        public int QueueDeep => _queue.Count;

        public void Handle(T order)
        {
            _queue.Enqueue(order);
        }

        public void Start()
        {
            var thread = new Thread(OrderHandler);
            _threadList.Add(thread);
            thread.Start();
        }

        

        private void OrderHandler()
        {
            while (true)
            {
                T order;
                if (_queue.TryDequeue(out order))
                {
                    _handlerOrder.Handle(order);
                    Thread.Sleep(100);
                }
            }
        }

        public void Dispose()
        {
            _threadList.ForEach(t =>
            {
                t.Abort();
            });
            _threadList = null;
        }
    }
}
