using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public class FakeBus : IBus
    {
        public FakeBus()
        {
            PublishedEvents = new List<IMessage>();
        }

        public void Publish(IMessage message)
        {
            PublishedEvents.Add(message);
        }

        public List<IMessage> PublishedEvents { get; set; }

        public void Clear()
        {
            PublishedEvents.Clear();
        }
    }
}
