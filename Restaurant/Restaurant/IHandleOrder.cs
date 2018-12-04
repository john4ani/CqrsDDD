using Restaurant.Models;

namespace Restaurant
{
    public interface IHandleOrder
    {
        void Handle(OrderDocument order);
    }
}
