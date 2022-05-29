using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace CulDeSac.Brokers.Queues
{
    public sealed partial class QueueBroker : IQueueBroker
    {
        private readonly IConfiguration configuration;

        public QueueBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            InitializeQueueClients();
        }


        private void InitializeQueueClients() =>
            this.StudentsQueue =
            GetQueueClient(queueName: nameof(StudentsQueue));


        //this is same as in storagebroker doing override onconfiguration
        //to get the connectionstring
        //parameter queueName because ur service can integrate with many queues
        private IQueueClient GetQueueClient(string queueName)
        {
            string connectionString =
                this.configuration.GetConnectionString(name: "ServiceBusConnection");

            //this is like using optionsbuilder.useSqL()
            return new QueueClient(connectionString, entityPath: queueName);
        }

        //this is configuration internally for the message broker, this is
        // my messagehandler options and if an exception occurs go ahead
        //and handle my exception this way(ExceptionReceivedEventHandler)
        private MessageHandlerOptions GetMessageHandlerOptions()
        {
            return new MessageHandlerOptions(ExceptionReceivedEventHandler)
            {
                AutoComplete = false,
                MaxConcurrentCalls = 1
            };
        }

        //when it fails to integrate with the queue do something with it
        private Task ExceptionReceivedEventHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            ExceptionReceivedContext exceptionReceivedContext =
                exceptionReceivedEventArgs.ExceptionReceivedContext;
            
            // do something here

            return Task.CompletedTask;
        }
    }
}
