using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Models;

namespace Restaurant
{
    public class TopicBasedPubSub
    {
        readonly Dictionary<string,dynamic> _topicSubscription = new Dictionary<string, dynamic>();

        public void Publish<T>(T order)
            where T : Message
        {
            var list =  _topicSubscription[typeof(T).FullName] as List<IHandle<T>>;
            foreach (var handle in list)
            {
                handle.Handle(order);
            }
        }
        
        public void Subscribe<T>(IHandle<T> orderHandler)
            where T : Message
        {
            var topic = typeof(T).FullName;
            Subscribe(orderHandler, topic);
        }

        private void Subscribe<T>(IHandle<T> orderHandler, string topic) where T : Message
        {
            if (!_topicSubscription.ContainsKey(topic))
                _topicSubscription.Add(topic, new List<IHandle<T>>());

            _topicSubscription[topic].Add(orderHandler);
        }

        public void SubscribeToColerationId<T>(Guid id, IHandle<T> handler)
            where T: Message
        {
            try
            {
                Subscribe(handler, id.ToString());
            }
            catch (Exception e)
            {
                //coleration is related to multiple types, only first will work
                //should not be used in prod - add type in key as well
            }
            
        }
    }
}
