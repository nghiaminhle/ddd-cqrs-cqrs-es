using CQRS.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Orders.Commands
{
    public class CancelOrder : ICommand
    {
        public CancelOrder()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid OrderId { get; set; }

        public Guid Id
        {
            get;
            private set;
        }
    }
}
