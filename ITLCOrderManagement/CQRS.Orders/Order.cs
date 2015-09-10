using CQRS.Infrastructure.Database;
using CQRS.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Orders
{
    public class Order : IAggregateRoot, IEventPublisher
    {
        private List<IEvent> listEvents = new List<IEvent>();

        protected Order()
        {
        }
        public Order( Guid orderId )
        { 
        }

        public void CreateOrder()
        {
 
        }

        public void ConfirmOrder()
        { 
        }

        public void CancelOrder()
        { 
        }

        public int Status { get; private set; }

        public List<OrderItem> Items { get; private set; }

        public DateTime OrderDate { get; private set; }
        public string Description { get; private set; }

        public Guid Id
        {
            get;
            private set;
        }

        public IEnumerable<IEvent> Events
        {
            get
            {
                return this.listEvents;
            }
        }
    }
}
