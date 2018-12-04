using Restaurant.Models;

namespace Restaurant
{
    public interface IHandleOrderWithQueue<T> : IHandle<T> where T: Message
    {
        int QueueDeep { get; }
    }
}
