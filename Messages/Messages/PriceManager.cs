using System;
using System.Collections.Generic;
using System.Linq;

namespace Messages
{
    public class PriceManager : IHandle<PriceUpdated>, IHandle<Sell10Sec>
    {
        decimal _acquirePrice;
        IBus _bus;

        private decimal _sellThreshold;
        private List<decimal> _sell10SecWindow = new List<decimal>();
        private decimal _highWatermark;
        private List<decimal> _moveUp15SecWindow = new List<decimal>();

        public PriceManager(IBus bus, decimal acquiringPrice)
        {
            _acquirePrice = acquiringPrice;
            _bus = bus;
            _highWatermark = _acquirePrice;
            _sellThreshold = _highWatermark - 10;
            _bus.Publish(new Sell10Sec(_sellThreshold));
        }

        public void Handle(PriceUpdated priceUpdatedEvent)
        {
            _sell10SecWindow.Add(priceUpdatedEvent.Price);
            _bus.Publish(new SendToMeIn<RemoveFrom10SecWindow>(13, new RemoveFrom10SecWindow(priceUpdatedEvent.Price)));
            _moveUp15SecWindow.Add(priceUpdatedEvent.Price);
            _bus.Publish(new SendToMeIn<RemoveFrom15SecWindow>(11, new RemoveFrom15SecWindow(priceUpdatedEvent.Price)));
        }

        public void Handle(Sell10Sec removeFromLowWindowEvent)
        {
            if (_sell10SecWindow.Max() <= _sellThreshold)
            {
                _bus.Publish(new SellMessage());               
            }
            else
            {
                _sell10SecWindow.RemoveAt(0);
            }
        }

        public void Handle(MoveUp15Sec removeFromHighWindowEvent)
        {
            if (_moveUp15SecWindow.Min() >= _highWatermark)
            {
                _highWatermark = _moveUp15SecWindow.Min();
                _sellThreshold = _highWatermark - 10;
                _bus.Publish(new SellThresholdUpdate(_sellThreshold));
            }
            else
            {
                _moveUp15SecWindow.RemoveAt(0);
            }
        }

    }
}
