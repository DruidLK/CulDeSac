using CulDeSac.Models;
using Moq;
using Xunit;

namespace CulDeSac.Tests.Unit.ServiceTests.Orchestrations.StudentEvents
{
    public partial class StudentEventOrchestrationTest
    {
        [Fact]
        public void ShouldListenAndAddStudent()
        {
            // given - Arrange 
            Student randomStudent = CreateRandomStudent();
            Student incomingStudent = randomStudent;

            this.studentEventServiceMock.Setup(service =>
                 service.ListenToStudentEvent(It.IsAny<Func<Student, ValueTask>>()))
                    .Callback<Func<Student, ValueTask>>(eventFunc =>
                        eventFunc.Invoke(incomingStudent));

            // when - act 
            this.studentEventOrchestrationService
                    .ListenToStudentEvents();

            // Then - Assert
            this.studentEventServiceMock.Verify(service =>
                service.ListenToStudentEvent(It.IsAny<Func<Student, ValueTask>>()),
                    Times.Once);

            this.studentServiceMock.Verify(service =>
                service.AddStudentAsync(incomingStudent),
                    Times.Once);

            this.studentEventServiceMock.VerifyNoOtherCalls();
            this.studentServiceMock.VerifyNoOtherCalls();
        }
    }
}
