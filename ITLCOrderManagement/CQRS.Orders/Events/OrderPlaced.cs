using CQRS.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Orders.Events
{
    public class OrderPlaced : IEvent
    {
        public Guid SourceId
        {
            get { throw new NotImplementedException(); }
        }
    }
}
