using CQRS.Infrastructure.Messaging.Handling;
using CQRS.Orders.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS.Infrastructure.Database;

namespace CQRS.Orders.Handler
{
    public class OrderCommandHandler : 
        ICommandHandler<PlaceOrder>,
        ICommandHandler<ConfirmOrder>,
        ICommandHandler<CancelOrder>
    {
        private Func<IDataContext<Order>> contextFactory;

        public OrderCommandHandler( Func<IDataContext<Order>> contextFactory )
        {
            this.contextFactory = contextFactory;
        }

        public void Handle( PlaceOrder command )
        {
            throw new NotImplementedException();
        }

        public void Handle( CancelOrder command )
        {
            throw new NotImplementedException();
        }

        public void Handle( ConfirmOrder command )
        {
            throw new NotImplementedException();
        }
    }
}
