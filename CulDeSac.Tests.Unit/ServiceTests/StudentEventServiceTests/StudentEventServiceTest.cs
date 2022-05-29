using System.Linq.Expressions;
using System.Text;
using CulDeSac.Brokers.Queues;
using CulDeSac.Models;
using CulDeSac.Services.StudentEvents;
using KellermanSoftware.CompareNetObjects;
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
        private readonly ICompareLogic compareLogic;

        public StudentEventServiceTest()
        {
            this.queueBrokerMock = new Mock<IQueueBroker>();
            this.compareLogic = new CompareLogic();

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

        private Expression<Func<Student, bool>> SameStudentAs(Student expectedStudent)
        {
            return actualStudent =>
                this.compareLogic
                    .Compare(expectedStudent, actualStudent)
                        .AreEqual;
        }
    }
}
