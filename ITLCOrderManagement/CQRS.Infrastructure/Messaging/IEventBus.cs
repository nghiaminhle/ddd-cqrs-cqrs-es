using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Messaging
{
    public interface IEventBus
    {
        void Publish( Envelope<IEvent> @event );
        void Publish( IEnumerable<Envelope<IEvent>> events );
    }
}
