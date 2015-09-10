using CQRS.Infrastructure.Serialization;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Messaging
{
    public class CommandBus : ICommandBus
    {
        private readonly IMessageSender sender;
        private readonly ITextSerializer serializer;

        public CommandBus( IMessageSender sender, ITextSerializer serializer )
        {
            this.sender = sender;
            this.serializer = serializer;
        }

        public void Send( Envelope<ICommand> command )
        {
            this.sender.Send( () => BuildMessage( command ) );
        }

        public void Send( IEnumerable<Envelope<ICommand>> commands )
        {
            foreach ( var command in commands )
            {
                this.Send( command );
            }
        }

        private BrokeredMessage BuildMessage( Envelope<ICommand> command )
        {
            var stream = new MemoryStream();
            try
            {
                var writer = new StreamWriter( stream );
                this.serializer.Serialize( writer, command.Body );
                stream.Position = 0;

                var message = new BrokeredMessage( stream, true );

                if ( !string.IsNullOrWhiteSpace( command.MessageId ) )
                {
                    message.MessageId = command.MessageId;
                }
                else if ( !default( Guid ).Equals( command.Body.Id ) )
                {
                    message.MessageId = command.Body.Id.ToString();
                }

                if ( !string.IsNullOrWhiteSpace( command.CorrelationId ) )
                {
                    message.CorrelationId = command.CorrelationId;
                }

                if ( command.Delay > TimeSpan.Zero )
                {
                    message.ScheduledEnqueueTimeUtc = DateTime.UtcNow.Add( command.Delay );
                }

                if ( command.TimeToLive > TimeSpan.Zero )
                {
                    message.TimeToLive = command.TimeToLive;
                }

                return message;
            }
            catch
            {
                stream.Dispose();
                throw;
            }
        }
    }
}
