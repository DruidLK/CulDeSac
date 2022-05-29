using CulDeSac.Models;
using Microsoft.Azure.ServiceBus;
using Moq;
using Xunit;

namespace CulDeSac.Tests.Unit.ServiceTests.StudentEventServiceTests
{
    public partial class StudentEventServiceTest
    {
        [Fact]
        public void ShouldListenToStudentEvent()
        {
            // given - arrange this is a mock of a function
            var studentEventHandlerMock =
                new Mock<Func<Student, ValueTask>>();

            Student randomStudent = CreateRandomStudent();
            Student incomingStudent = randomStudent;
            Message studentMessage = CreateStudentMessage(incomingStudent);

            this.queueBrokerMock.Setup(broker =>
                broker.ListenToStudentsQueue(
                    It.IsAny<Func<Message, CancellationToken, Task>>()))
                        .Callback<Func<Message, CancellationToken, Task>>(eventFunc =>
                            eventFunc.Invoke(studentMessage, It.IsAny<CancellationToken>()));

            // when - act
            studentEventService.ListenToStudentEvent(
                studentEventHandler: studentEventHandlerMock.Object);

            // then - assert
            studentEventHandlerMock.Verify(handler =>
                handler.Invoke(
                    It.Is(SameStudentAs(incomingStudent))),
                      Times.Once);

            this.queueBrokerMock.Verify(broker =>
                broker.ListenToStudentsQueue(
                    It.IsAny<Func<Message, CancellationToken, Task>>()),
                        Times.Once);

            this.queueBrokerMock.VerifyNoOtherCalls();
        }
    }
}
