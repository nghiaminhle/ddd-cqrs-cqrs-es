using CQRS.Infrastructure.Serialization;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Messaging.Handling
{
    public abstract class MessageProcessor : IProcessor
    {
        private readonly IMessageReceiver receiver;
        private readonly ITextSerializer serializer;
        private readonly object lockObject = new object();
        private bool started = false;

        public MessageProcessor(IMessageReceiver receiver, ITextSerializer serializer)
        {
            this.receiver = receiver;
            this.serializer = serializer;
        }

        public void Start()
        {
            lock ( this.lockObject )
            {
                this.receiver.Start( OnMessageReceived );
                this.started = true;
            }
        }

        public void Stop()
        {
            lock ( this.lockObject )
            {
                this.receiver.Stop();
                this.started = false;
            }
        }

        public abstract void ProcessMessage( object payload, string messageId, string correlationId );
        

        private void OnMessageReceived( BrokeredMessage message )
        {
            object payload;
            using ( var stream = message.GetBody<Stream>() )
            using ( var reader = new StreamReader( stream ) )
            {
                try
                {
                    payload = this.serializer.Deserialize( reader );
                }
                catch ( SerializationException e )
                {
                    message.DeadLetter( e.Message, e.ToString() );
                    return;
                }
            }

            ProcessMessage( payload, message.MessageId, message.CorrelationId );
            message.Complete();
        }
    }
}
