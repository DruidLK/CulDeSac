using System.Text;
using CulDeSac.Brokers.Queues;
using CulDeSac.Models;
using CulDeSac.Services.StudentEvents;
using Microsoft.Azure.ServiceBus;
using Moq;
using Newtonsoft.Json;
using Tynamix.ObjectFiller;

namespace CulDeSac.Tests.Unit.ServiceTests.StudentEventServiceTests
{
    public partial class StudentEventServiceTest
    {
        private readonly Mock<IQueueBroker> queueBrokerMock;
        private readonly IStudentEventService studentEventService;

        public StudentEventServiceTest()
        {
            this.queueBrokerMock = new Mock<IQueueBroker>();

            this.studentEventService = new StudentEventService(
                queueBroker: this.queueBrokerMock.Object);
        }

        private static Message CreateStudentMessage(Student student)
        {
            string serializeStudent =
                JsonConvert.SerializeObject(value: student);

            byte[] studentBody =
                Encoding.UTF8.GetBytes(serializeStudent);

            return new Message
            {
                Body = studentBody
            };

        }
        private static Student CreateRandomStudent() =>
            CreateRandomFiller().Create();

        private static Filler<Student> CreateRandomFiller() =>
            new Filler<Student>();
    }
}
