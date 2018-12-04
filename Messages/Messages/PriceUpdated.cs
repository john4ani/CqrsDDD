using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public class PriceUpdated : IMessage
    {
        public PriceUpdated(decimal price)
        {
            Price = price;
        }

        public decimal Price { get; set; }
    }
}
