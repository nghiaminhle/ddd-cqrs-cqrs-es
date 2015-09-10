using CQRS.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Database
{
    public class SqlDataContext<T> : IDataContext<T> where T : class, IAggregateRoot
    {
        private readonly IEventBus eventBus;
        private readonly DbContext context;

        public SqlDataContext( Func<DbContext> contextFactory, IEventBus eventBus )
        {
            this.eventBus = eventBus;
            this.context = contextFactory.Invoke();
        }

        public T Find( Guid id )
        {
            return this.context.Set<T>().Find( id );
        }

        public void Save( T aggregateRoot )
        {
            var entry = this.context.Entry( aggregateRoot );

            if ( entry.State == System.Data.Entity.EntityState.Detached )
                this.context.Set<T>().Add( aggregateRoot );

            // Can't have transactions across storage and message bus.
            this.context.SaveChanges();

            var eventPublisher = aggregateRoot as IEventPublisher;
            
            if ( eventPublisher != null )
                this.eventBus.Publish( eventPublisher.Events.Select( x => new Envelope<IEvent>( x ) ) );
        }

        public void Dispose()
        {
            this.Dispose( true );
            GC.SuppressFinalize( this );
        }

        ~SqlDataContext()
        {
            this.Dispose( false );
        }

        protected virtual void Dispose( bool disposing )
        {
            if ( disposing )
            {
                this.context.Dispose();
            }
        }
    }
}
