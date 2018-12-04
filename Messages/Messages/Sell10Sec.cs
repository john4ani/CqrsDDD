using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public class Sell10Sec : IMessage
    {
        public Sell10Sec(decimal price)
        {
            Price = price;
        }
        public decimal Price { get; set; }
    }
}
