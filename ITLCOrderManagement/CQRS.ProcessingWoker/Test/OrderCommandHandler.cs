using CQRS.Infrastructure.Messaging.Handling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.ProcessingWoker.Test
{
    public class OrderCommandHandler : 
        ICommandHandler<PlaceOrder>
    {
        public void Handle( PlaceOrder command )
        {
            Console.WriteLine( command.ProductId );
        }
    }
}
