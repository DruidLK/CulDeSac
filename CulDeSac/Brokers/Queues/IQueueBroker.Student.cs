using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace CulDeSac.Brokers.Queues
{
    public partial interface IQueueBroker
    {
        // will take a message
        //which is a byte[]
        void ListenToStudentsQueue(Func<Message, CancellationToken, Task> eventHandler);
    }
}
