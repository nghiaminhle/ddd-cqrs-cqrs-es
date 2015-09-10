using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Messaging
{
    public interface ICommandBus
    {
        void Send( Envelope<ICommand> command );
        void Send( IEnumerable<Envelope<ICommand>> commands );
    }
}
