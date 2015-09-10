using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Messaging
{
    public class TopicSender : IMessageSender
    {
        private TopicClient topicClient;

        public TopicSender(TopicClient topicClient)
        {
            this.topicClient = topicClient;
        }

        public void Send( Func<Microsoft.ServiceBus.Messaging.BrokeredMessage> messageFactory )
        {
            this.topicClient.Send( messageFactory() );
        }
    }
}
