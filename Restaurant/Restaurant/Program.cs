using System;
using System.Collections.Generic;
using System.Threading;
using Restaurant.Actors;
using Restaurant.Commands;
using Restaurant.Events;
using AssistantManager = Restaurant.ActorsForTopicPubSub.AssistantManager;
using Cashier = Restaurant.ActorsForTopicPubSub.Cashier;
using Cook = Restaurant.ActorsForTopicPubSub.Cook;
using Waiter = Restaurant.ActorsForTopicPubSub.Waiter;

namespace Restaurant
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = new TopicBasedPubSub();
            var printer = new OrderPrinter(bus);
            var cashier = new Cashier(bus);
            var am = new ThreadedHandler<PriceOrder>(new AssistantManager(bus));
            var cookedOrder = new HashSet<Guid>();
            var cook1 = new ThreadedHandler<CookFood>(new Cook(bus, "jim1", cookedOrder));
            var cook2 = new ThreadedHandler<CookFood>(new Cook(bus, "jim2", cookedOrder));
            var cook3 = new ThreadedHandler<CookFood>(new Cook(bus, "jim3", cookedOrder));
            var monkey = new Monkey(bus);
            var alarmClock = new AlarmClock(bus);
            var dispatcher = new ThreadedHandler<CookFood>(new MoreFairDispatcher<CookFood>(new List<IHandle<CookFood>> { cook1, cook2, cook3 }));
            var waiter = new Waiter(bus);

            //subscribe
            bus.Subscribe(dispatcher);
            bus.Subscribe(cashier);
            bus.Subscribe(printer);
            bus.Subscribe(am);
            bus.Subscribe(monkey);
            bus.Subscribe(alarmClock);

            //start
            cook1.Start();
            cook2.Start();
            cook3.Start();
            am.Start();
            dispatcher.Start();

            for (int i = 0;i<100;i++)
            {
                var guid = Guid.NewGuid();
                waiter.PlaceOrder(guid);
            }
            while (true)
            {
                Console.WriteLine("Waiting for payments..");
                Console.WriteLine("AM queue deep : " + am.QueueDeep);
                Console.WriteLine("C1 queue deep : " + cook1.QueueDeep);
                Console.WriteLine("C2 queue deep : " + cook2.QueueDeep);
                Console.WriteLine("C3 queue deep : " + cook3.QueueDeep);
                Console.WriteLine("Dispatcher queue deep : " + dispatcher.QueueDeep);
                
                cashier.UnpaiedOrders.ForEach(o=>cashier.Pay(o.Id));
                Thread.Sleep(100);
            }
            Console.ReadKey();
        }
    }
}
