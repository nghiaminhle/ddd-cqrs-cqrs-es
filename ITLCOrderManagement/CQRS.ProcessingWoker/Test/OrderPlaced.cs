using CQRS.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.ProcessingWoker.Test
{
    public class OrderPlaced : IEvent
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public Guid SourceId
        {
            get;
            set;
        }
    }
}
