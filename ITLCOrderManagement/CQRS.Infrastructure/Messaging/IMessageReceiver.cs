using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Messaging
{
    public interface IMessageReceiver
    {
        /// <summary>
        /// Starts the listener.
        /// </summary>
        /// <param name="messageHandler">Handler for incoming messages. The return value indicates how to release the message lock.</param>
        void Start( Action<BrokeredMessage> messageHandler );

        /// <summary>
        /// Stops the listener.
        /// </summary>
        void Stop();
    }
}
