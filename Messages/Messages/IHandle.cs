using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public interface IHandle<T>  where T : IMessage
    {
        void Handle(T message);
    }
}
