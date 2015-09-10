using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Orders.Database
{
    public class OrderDbContext:DbContext
    {
        public const string SchemaName = "Order";

        public OrderDbContext( string connectionString )
            : base( connectionString )
        {
            this.Orders = base.Set<Order>();
        }

        protected override void OnModelCreating( DbModelBuilder modelBuilder )
        {
            base.OnModelCreating( modelBuilder );
            modelBuilder.Entity<Order>().ToTable( "Orders", SchemaName );
            modelBuilder.Entity<Order>().HasKey( o => o.Id );
        }

        public DbSet<Order> Orders
        {
            get;
            private set;
        }
    }
}
