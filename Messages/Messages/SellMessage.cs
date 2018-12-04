using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public class SellMessage : IMessage
    {
        public decimal Price { get; set; }
    }
}
