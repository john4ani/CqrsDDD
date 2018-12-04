using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Commands;
using Restaurant.Events;

namespace Restaurant.ProcessManager
{
    public class MidgetHouse : IHandle<OrderPlaced>
    {
        private readonly TopicBasedPubSub _bus;
        public Dictionary<Guid,Midget<OrderPlaced>> Midgets { get; set; }

        public MidgetHouse(TopicBasedPubSub bus)
        {
            _bus = bus;
        }

        public void Handle(OrderPlaced @event)
        {
            if(!Midgets.ContainsKey(@event.ColerationId))
                Midgets.Add(@event.ColerationId,new OrderPlacedMidget(_bus));

            Midgets[@event.ColerationId].Handle(@event);
            _bus.SubscribeToColerationId(@event.ColerationId, Midgets[@event.ColerationId]);
        }

        
    }

    public class OrderPlacedMidget : Midget<OrderPlaced>
    {
        private readonly TopicBasedPubSub _bus;

        public OrderPlacedMidget(TopicBasedPubSub bus)
        {
            _bus = bus;
        }

        public override void Handle(OrderPlaced @event)
        {
            _bus.Publish(new CookFood {ColerationId = @event.ColerationId, Order = @event.Order});
        }
    }

    public abstract class Midget<T> : IHandle<T>
        where T : Message
    {
        public abstract void Handle(T @event);
        
    }


}
