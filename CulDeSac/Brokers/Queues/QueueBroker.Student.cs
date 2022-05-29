using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace CulDeSac.Brokers.Queues
{
    public sealed partial class QueueBroker : IQueueBroker
    {
        //match the name with the resource group of the queue name
        //that u named
        //this is like DBSet<> of the table
        public IQueueClient StudentsQueue { get; set; }

        //when u receive a message and it was successfull u have to tell the queue
        //hey queue i got it 
        public void ListenToStudentsQueue(Func<Message, CancellationToken, Task> eventHandler)
        {
            //messagehandler options that says how many times
            //we have to retry and all that
            
            MessageHandlerOptions messageHandlerOptions =
                GetMessageHandlerOptions();

            //whatever is passing down to this, we are just appending to it, since
            //the handler cannot now what to do after passing the message

            Func<Message, CancellationToken, Task> messageEventHandler =
                CompleteStudentsQueueMessageAsync(handler: eventHandler);


            //i am subscribing here , start listening to the queue
            //and if a message is there i will be aware of it
            this.StudentsQueue.RegisterMessageHandler(messageEventHandler, messageHandlerOptions);
        }

        //i am saying here after the message is handled wether is success or fail release the lock
        //on the message 
        private Func<Message, CancellationToken, Task> CompleteStudentsQueueMessageAsync(
            Func<Message, CancellationToken, Task> handler) =>
                async (message, token) =>
            {
                await handler(message, token);
                await this.StudentsQueue.CompleteAsync(lockToken: message.SystemProperties.LockToken);
            };
        }
    }
