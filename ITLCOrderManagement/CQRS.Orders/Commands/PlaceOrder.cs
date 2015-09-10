using CQRS.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Orders.Commands
{
    public class PlaceOrder : ICommand
    {
        public PlaceOrder()
        {
            this.Id = Guid.NewGuid();
        }

        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public string Description { get; set; }

        public Guid Id
        {
            get;
            private set;
        }
    }
}
