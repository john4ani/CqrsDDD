using Restaurant.Commands;
using Restaurant.Events;
using Restaurant.Models;

namespace Restaurant.ActorsForTopicPubSub
{
    public class AssistantManager : IHandle<PriceOrder>
    {
        private readonly TopicBasedPubSub _orderHandler;

        public AssistantManager(TopicBasedPubSub orderHandler)
        {
            _orderHandler = orderHandler;
        }
        public void Handle(PriceOrder @event)
        {
            @event.Order.SubTotal = 90;
            @event.Order.Total = 100;
            @event.Order.Tax = 10;
            //or look prices in a db
            _orderHandler.Publish(new OrderPriced {Order = @event.Order });
        }
    }
}
