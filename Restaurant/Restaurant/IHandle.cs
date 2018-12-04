using System;
using Restaurant.Models;

namespace Restaurant
{
    public interface IHandle<T> where T: Message
    {
        void Handle(T @event);
    }

    /// <summary>
    /// An event/message
    /// </summary>
    public class Message
    {
        public OrderDocument Order { get; set; }
        public Guid ColerationId { get; set; }
    }
}
