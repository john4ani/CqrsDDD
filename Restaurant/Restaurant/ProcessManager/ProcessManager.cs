

using Restaurant.Events;

namespace Restaurant.ProcessManager
{
    public class ProcessManager : IHandle<FoodCoocked>, IHandle<OrderPriced>, IHandle<OrderPaid>, IHandle<OrderPlaced>
    {
        private readonly TopicBasedPubSub _bus;

        public ProcessManager(TopicBasedPubSub bus)
        {
            _bus = bus;
        }

        public void Handle(FoodCoocked @event)
        {
            throw new System.NotImplementedException();
        }

        public void Handle(OrderPriced @event)
        {
            throw new System.NotImplementedException();
        }

        public void Handle(OrderPaid @event)
        {
            throw new System.NotImplementedException();
        }

        public void Handle(OrderPlaced @event)
        {
            throw new System.NotImplementedException();
        }
    }
}
