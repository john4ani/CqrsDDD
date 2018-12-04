

namespace Messages
{
    public interface IBus
    {
        void Publish(IMessage message);
    }
}
