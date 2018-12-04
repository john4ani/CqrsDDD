using NUnit.Framework;
using System.Linq;

namespace Messages
{
    [TestFixture]
    public class PriceManagerTests
    {
        FakeBus _bus;
        decimal _acquiringPrice;
        PriceManager _priceManager;

        [SetUp]
        public void SetUp()
        {
            _bus = new FakeBus();
            _acquiringPrice = 100;
            _priceManager = new PriceManager(_bus, _acquiringPrice);
        }

        [Test]
        public void PriceManager_Receives_Price_Updated_Event_Publishes_Reminder_Events()
        {
            //given initialised Price Manager 
            //when Price Updated event arrices
            //then  - Remindme(13, droppricefrom10secwindow)
            //      - Remindme(11, droppricefrom15secwindow)

            // arrange
            var message = new PriceUpdated(105);


            // act
            _priceManager.Handle(message);

            // assert
            Assert.That(_bus.PublishedEvents.Count, Is.EqualTo(3));

            var removeSellThresholdSetEvent = (Sell10Sec) _bus.PublishedEvents.First(evt => evt.GetType() == typeof(Sell10Sec));

            Assert.That(removeSellThresholdSetEvent.Price, Is.EqualTo(90));
            var removeFromHighWindowReminderEvent = (SendToMeIn<RemoveFrom15SecWindow>)
                _bus.PublishedEvents.First(evt => evt.GetType() == typeof(SendToMeIn<RemoveFrom15SecWindow>));

            Assert.That(removeFromHighWindowReminderEvent.Message, Is.TypeOf<RemoveFrom15SecWindow>());
            Assert.That(removeFromHighWindowReminderEvent.Message.Price, Is.EqualTo(105));
            Assert.That(removeFromHighWindowReminderEvent.Seconds, Is.EqualTo(11));

            var removeFromLowWindowReminderEvent =
                (SendToMeIn<RemoveFrom10SecWindow>)
                _bus.PublishedEvents.Single(evt => evt.GetType() == typeof(SendToMeIn<RemoveFrom10SecWindow>));

            Assert.That(removeFromLowWindowReminderEvent.Message, Is.TypeOf<RemoveFrom10SecWindow>());
            Assert.That(removeFromLowWindowReminderEvent.Message.Price, Is.EqualTo(105));
            Assert.That(removeFromLowWindowReminderEvent.Seconds, Is.EqualTo(13));


        }

        [Test]
        public void ProcessManager_Recieves_Reminder_Event_Publishes_SellEvent()
        {
            //arrange 
            var priceUpdatedMessage = new PriceUpdated(80);
            _priceManager.Handle(priceUpdatedMessage);
            var removeFromLowWindow = new Sell10Sec(80);
            _bus.Clear();

            //act 
            _priceManager.Handle(removeFromLowWindow);

            // assert
            Assert.That(_bus.PublishedEvents.Count, Is.EqualTo(1));
            var sellEvent = (SellMessage)
                _bus.PublishedEvents.SingleOrDefault(evt => evt.GetType() == typeof(SellMessage));
            Assert.That(sellEvent, Is.Not.Null);

        }

        [Test]
        public void ProcessManager_Recieves_Reminder_Event_Does_Not_Publishe_SellEvent()
        {
            //arrange 
            var priceUpdatedMessage = new PriceUpdated(99);
            _priceManager.Handle(priceUpdatedMessage);
            var removeFromLowWindow = new Sell10Sec(99);
            _bus.Clear();

            //act 
            _priceManager.Handle(removeFromLowWindow);

            // assert
            Assert.That(_bus.PublishedEvents.Count, Is.EqualTo(0));

        }
    }
}
