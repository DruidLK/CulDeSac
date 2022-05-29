using System;
using System.Threading.Tasks;
using CulDeSac.Brokers.Queues;
using CulDeSac.Models;

namespace CulDeSac.Services.StudentEvents
{
    public class StudentEventService : IStudentEventService
    {
        private readonly IQueueBroker queueBroker;

        public StudentEventService(IQueueBroker queueBroker) =>
            this.queueBroker = queueBroker;

        public void ListenToStudentEvent(Func<Student, ValueTask> studentEventHandler )
        {
            throw new NotImplementedException();
        }
    }
}
