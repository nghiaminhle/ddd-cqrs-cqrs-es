using CQRS.Infrastructure.Messaging.Handling;
using CQRS.Orders.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Orders.Handler
{
    public class OrderViewGenerator : 
        IEventHandler<OrderCanceled>,
        IEventHandler<OrderConfirmed>,
        IEventHandler<OrderPlaced>
    {
        public void Handle( OrderCanceled @event )
        {
            throw new NotImplementedException();
        }

        public void Handle( OrderConfirmed @event )
        {
            throw new NotImplementedException();
        }

        public void Handle( OrderPlaced @event )
        {
            throw new NotImplementedException();
        }
    }
}
