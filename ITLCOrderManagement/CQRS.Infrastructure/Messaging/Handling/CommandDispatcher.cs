using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Messaging.Handling
{
    public class CommandDispatcher
    {
        private Dictionary<Type, ICommandHandler> handlers = new Dictionary<Type, ICommandHandler>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandDispatcher"/> class.
        /// </summary>
        public CommandDispatcher()
        {
        }

        /// <summary>
        /// Registers the specified command handler.
        /// </summary>
        public void Register( ICommandHandler commandHandler )
        {
            var genericHandler = typeof( ICommandHandler<> );
            var supportedCommandTypes = commandHandler.GetType()
                .GetInterfaces()
                .Where( iface => iface.IsGenericType && iface.GetGenericTypeDefinition() == genericHandler )
                .Select( iface => iface.GetGenericArguments()[ 0 ] )
                .ToList();

            if ( handlers.Keys.Any( registeredType => supportedCommandTypes.Contains( registeredType ) ) )
                throw new ArgumentException( "The command handled by the received handler already has a registered handler." );

            // Register this handler for each of he handled types.
            foreach ( var commandType in supportedCommandTypes )
            {
                this.handlers.Add( commandType, commandHandler );
            }
        }

        /// <summary>
        /// Processes the message by calling the registered handler.
        /// </summary>
        public bool ProcessMessage( ICommand payload, string messageId, string correlationId )
        {
            var commandType = payload.GetType();
            ICommandHandler handler = null;

            if ( this.handlers.TryGetValue( commandType, out handler ) )
            {
                ( (dynamic)handler ).Handle( (dynamic)payload );
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
