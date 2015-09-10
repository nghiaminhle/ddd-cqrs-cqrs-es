using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Messaging.Handling
{
    public interface IEventHandlerRegistry
    {
        void Register( IEventHandler handler );
    }
}
