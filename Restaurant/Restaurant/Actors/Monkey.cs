using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Events;

namespace Restaurant.Actors
{
    public class Monkey : IHandle<OrderPlaced>
    {
        private readonly Random _random;
        private readonly TopicBasedPubSub _bus;

        public Monkey(TopicBasedPubSub bus)
        {
            _bus = bus;
            _random = new Random();
        }

        public void Handle(OrderPlaced message)
        {
            var randomIndex = _random.Next(0, 9);

            var randomMessage = CreateMessage(randomIndex, message);
            if (randomMessage != null)
            {
                randomMessage.Order.Id = Guid.NewGuid();
                randomMessage.ColerationId = message.ColerationId;

                var randomDelay = _random.Next(0, 10000);
                Task.Delay(randomDelay).ContinueWith(i => _bus.Publish(randomMessage));
            }
        }

        private static Message CreateMessage(int index, OrderPlaced orderPlaced)
        {
            switch (index)
            {
                case 0:
                    Console.WriteLine("Evil Actor: Sending Order Cooked {0}", orderPlaced.Order.Id);
                    return new FoodCoocked {Order = orderPlaced.Order};
                case 1:
                    Console.WriteLine("Evil Actor: Sending Order Dropped {0}", orderPlaced.Order.Id);
                    //return new OrderDropped(orderPlaced.Order);
                    break;
                case 2:
                    Console.WriteLine("Evil Actor: Sending Order Paid {0}", orderPlaced.Order.Id);
                    return new OrderPaid {Order = orderPlaced.Order};
                case 3:
                    Console.WriteLine("Evil Actor: Sending Order Completed {0}", orderPlaced.Order.Id);
                    //return new OrderCompleted(orderPlaced.Order);
                    break;

            }
            return null;
        }
    }
}
