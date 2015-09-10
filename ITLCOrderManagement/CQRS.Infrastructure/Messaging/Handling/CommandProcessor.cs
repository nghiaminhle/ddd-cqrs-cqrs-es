using CQRS.Infrastructure.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Messaging.Handling
{
    public class CommandProcessor : MessageProcessor
    {
        private readonly CommandDispatcher commandDispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandProcessor"/> class.
        /// </summary>
        /// <param name="receiver">The receiver to use. If the receiver is <see cref="IDisposable"/>, it will be disposed when the processor is 
        /// disposed.</param>
        /// <param name="serializer">The serializer to use for the message body.</param>
        public CommandProcessor( IMessageReceiver receiver, ITextSerializer serializer )
            : base( receiver, serializer )
        {
            this.commandDispatcher = new CommandDispatcher();
        }

        /// <summary>
        /// Registers the specified command handler.
        /// </summary>
        public void Register( ICommandHandler commandHandler )
        {
            this.commandDispatcher.Register( commandHandler );
        }

        public override void ProcessMessage( object payload, string messageId, string correlationId )
        {
            this.commandDispatcher.ProcessMessage((ICommand)payload, messageId, correlationId );
        }
    }
}
