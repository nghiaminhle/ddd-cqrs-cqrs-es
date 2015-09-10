using CQRS.Infrastructure.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Messaging.Handling
{
    public class EventProcessor : MessageProcessor
    {
        private readonly EventDispatcher eventDispatcher;

        public EventProcessor( IMessageReceiver receiver, ITextSerializer serializer )
            : base( receiver, serializer )
        {
            this.eventDispatcher = new EventDispatcher();
        }

        public void Register( IEventHandler eventHandler )
        {
            this.eventDispatcher.Register( eventHandler );
        }

        public override void ProcessMessage( object payload, string messageId, string correlationId )
        {
            var @event = payload as IEvent;
            if ( @event != null )
            {
                this.eventDispatcher.DispatchMessage( @event, messageId, correlationId );
            }
        }
    }
}
