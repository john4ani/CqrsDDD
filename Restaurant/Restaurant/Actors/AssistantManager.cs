using Restaurant.Models;

namespace Restaurant.Actors
{
    public class AssistantManager : IHandleOrder
    {
        private readonly IHandleOrder _orderHandler;

        public AssistantManager(IHandleOrder orderHandler)
        {
            _orderHandler = orderHandler;
        }
        public void Handle(OrderDocument order)
        {
            order.SubTotal = 90;
            order.Total = 100;
            order.Tax = 10;
            //or look prices in a db
            _orderHandler.Handle(order);
        }
    }
}
