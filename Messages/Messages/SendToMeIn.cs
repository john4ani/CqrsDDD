using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public class SendToMeIn<T> : IMessage
        where T : IMessage
    {
        public SendToMeIn(int seconds, T message)
        {
            Seconds = seconds;
            Message = message;
        }

        public int Seconds { get; set; }

        public T Message { get; set; }

        public decimal Price
        {
            get; set;
        }
    }
}
