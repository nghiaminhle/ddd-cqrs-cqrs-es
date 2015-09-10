using CQRS.Infrastructure.Messaging.Handling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.ProcessingWoker.Test
{
    public class OrderEventHandler : 
        IEventHandler<OrderPlaced>
    {
        public void Handle( OrderPlaced @event )
        {
            Console.WriteLine( @event.ProductId );
        }
    }
}
