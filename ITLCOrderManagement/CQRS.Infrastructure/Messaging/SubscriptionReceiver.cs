using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Messaging
{
    public class SubscriptionReceiver : IMessageReceiver
    {
        private static readonly TimeSpan ReceiveLongPollingTimeout = TimeSpan.FromMinutes( 1 );
        private CancellationTokenSource cancellationSource;
        private Action<BrokeredMessage> messageHandler;
        private readonly object lockObject = new object();
        private SubscriptionClient client;

        public SubscriptionReceiver(SubscriptionClient subscriptionClient)
        {
            this.client = subscriptionClient;
        }

        public void Start( Action<BrokeredMessage> messageHandler )
        {
            lock ( this.lockObject )
            {
                this.cancellationSource = new CancellationTokenSource();
                this.messageHandler = messageHandler;
                Task.Factory.StartNew( () =>
                   this.ReceiveMessages( this.cancellationSource.Token ),
                   this.cancellationSource.Token );
            }
        }

        public void Stop()
        {
            lock ( this.lockObject )
            {
                using ( this.cancellationSource )
                {
                    if ( this.cancellationSource != null )
                    {
                        this.cancellationSource.Cancel();
                        this.cancellationSource = null;
                        this.messageHandler = null;
                    }
                }
            }
        }

        private void ReceiveMessages( CancellationToken cancellationToken )
        {
            while ( true )
            {
                BrokeredMessage message = this.client.Receive( ReceiveLongPollingTimeout );
                if ( message != null )
                {
                    this.messageHandler( message );
                }
            }
        }
    }
}
