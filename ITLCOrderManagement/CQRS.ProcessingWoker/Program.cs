using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using CQRS.Infrastructure.Messaging;
using CQRS.Infrastructure.Serialization;
using CQRS.Infrastructure.Messaging.Handling;
using CQRS.ProcessingWoker.Test;

namespace CQRS.ProcessingWoker
{
    class Program
    {
        static void Main( string[] args )
        {
            CreateTopic();
            //TestSendCommand();
            //TestProcessCommand();

            TestSendEvent();
            TestProcessEvent();

            Console.WriteLine( "Finish!" );
            Console.ReadKey();
        }

        #region Topic Settings
        static string serviceBusConnectionString = ConfigurationManager.AppSettings[ "Microsoft.ServiceBus.ConnectionString" ];
        static string commandTopicPath = "ITLC/OrderCommands";
        static string commandHandlerSubscription = "OrderCommandHandler";

        static string orderEventTopicPath = "ITLC/OrderEvents";
        static string orderEventSubscription = "OrderViewGenerator";

        static void CreateTopic()
        {
            NamespaceManager namespaceManager = NamespaceManager.CreateFromConnectionString( serviceBusConnectionString );
            if ( !namespaceManager.TopicExists( commandTopicPath ) )
            {
                namespaceManager.CreateTopic( commandTopicPath );
                namespaceManager.CreateSubscription( commandTopicPath, commandHandlerSubscription );
            }
            if ( !namespaceManager.TopicExists( orderEventTopicPath ) )
            {
                namespaceManager.CreateTopic( orderEventTopicPath );
                namespaceManager.CreateSubscription( orderEventTopicPath, orderEventSubscription );
            }
            
            
        }

        static void TestSendCommand()
        {
            TopicClient commandClient = TopicClient.CreateFromConnectionString( serviceBusConnectionString, commandTopicPath );
            TopicSender sender = new TopicSender( commandClient );
            //sender.Send( () => { return new BrokeredMessage( "Hello CQRS" ); } );
            ITextSerializer serializer = new JsonTextSerializer();
            CommandBus bus = new CommandBus( sender, serializer );

            bus.Send( new Envelope<ICommand>( new PlaceOrder() { ProductId = 1, Quantity = 10 } ) );
        }

        static void TestProcessCommand()
        {
            SubscriptionClient subscriptionClient = SubscriptionClient.CreateFromConnectionString( serviceBusConnectionString, commandTopicPath, commandHandlerSubscription );
            SubscriptionReceiver receiver = new SubscriptionReceiver( subscriptionClient );
            ITextSerializer serializer = new JsonTextSerializer();

            CommandProcessor orderCommandProcessor = new CommandProcessor( receiver, serializer );
            orderCommandProcessor.Register( new OrderCommandHandler() );

            orderCommandProcessor.Start();
        }

        static void TestSendEvent()
        {
            TopicClient eventClient = TopicClient.CreateFromConnectionString( serviceBusConnectionString, orderEventTopicPath );
            TopicSender sender = new TopicSender( eventClient );
            ITextSerializer serializer = new JsonTextSerializer();

            EventBus eventBus = new EventBus( sender, serializer );
            eventBus.Publish( new Envelope<IEvent>( new OrderPlaced()
            {
                ProductId = 1,
                Quantity = 2,
                SourceId = Guid.NewGuid()
            } ) );
        }

        static void TestProcessEvent()
        {
            SubscriptionClient subscriptionClient = SubscriptionClient.CreateFromConnectionString( serviceBusConnectionString, orderEventTopicPath, orderEventSubscription );
            SubscriptionReceiver receiver = new SubscriptionReceiver( subscriptionClient );
            ITextSerializer serializer = new JsonTextSerializer();

            EventProcessor eventProcessor = new EventProcessor( receiver, serializer );
            eventProcessor.Register( new OrderEventHandler() );
            eventProcessor.Start();
        }

        static void TestMessageHandler( BrokeredMessage message )
        {
            Console.WriteLine( message.GetBody<string>() );
            message.Complete();
        }
        #endregion
    }
}
