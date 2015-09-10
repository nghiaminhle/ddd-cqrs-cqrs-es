using CQRS.Infrastructure.Serialization;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Messaging
{
    public class EventBus : IEventBus
    {
        private readonly IMessageSender sender;
        private readonly ITextSerializer serializer;

        public EventBus( IMessageSender sender, ITextSerializer serializer )
        {
            this.sender = sender;
            this.serializer = serializer;
        }

        public void Publish( Envelope<IEvent> @event )
        {
            this.sender.Send( () => BuildMessage( @event ) );
        }

        public void Publish( IEnumerable<Envelope<IEvent>> events )
        {
            foreach ( var @event in events )
            {
                this.Publish( @event );
            }
        }

        private BrokeredMessage BuildMessage( Envelope<IEvent> envelope )
        {
            var @event = envelope.Body;

            var stream = new MemoryStream();
            try
            {
                var writer = new StreamWriter( stream );
                this.serializer.Serialize( writer, @event );
                stream.Position = 0;

                var message = new BrokeredMessage( stream, true );

                message.SessionId = @event.SourceId.ToString();

                if ( !string.IsNullOrWhiteSpace( envelope.MessageId ) )
                {
                    message.MessageId = envelope.MessageId;
                }

                if ( !string.IsNullOrWhiteSpace( envelope.CorrelationId ) )
                {
                    message.CorrelationId = envelope.CorrelationId;
                }

                return message;
            }
            catch
            {
                stream.Dispose();
                throw;
            }
        }
    }
}
