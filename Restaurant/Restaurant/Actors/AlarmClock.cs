using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Restaurant.Events;

namespace Restaurant.Actors
{
    public class AlarmClock : IHandle<DelyPublish>
    {
        private readonly TopicBasedPubSub _bus;
        private SortedList<DateTime,Message> _messages;

        public AlarmClock(TopicBasedPubSub bus)
        {
            _bus = bus;
            _messages = new SortedList<DateTime, Message>();
            Task.Factory.StartNew(Do);
        }

        public void Handle(DelyPublish @event)
        {
            _messages.Add(@event.Until,@event);
        }

        private void Do()
        {
            while (true)
            {
                foreach (var message in _messages.Where(m => m.Key <= DateTime.Now))
                {
                    _bus.Publish(message.Value);
                }
                Thread.Sleep(300);
            }
        }
    }

    public class DelyPublish : Message
    {
        public DateTime Until{ get; set; }
        public Message Message { get; set; }
    }
}
