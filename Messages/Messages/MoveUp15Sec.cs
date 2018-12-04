using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public class MoveUp15Sec : IMessage
    {
        public decimal Price { get; set;  }

        public MoveUp15Sec(decimal price)
        {
           Price = price;
        }
    }
}
