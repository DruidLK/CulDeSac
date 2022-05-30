using CulDeSac.Models;
using CulDeSac.Services.OrchestrationServices.StudentEvents;
using CulDeSac.Services.StudentEvents;
using CulDeSac.Services.StudentServices;
using Moq;
using Tynamix.ObjectFiller;

namespace CulDeSac.Tests.Unit.ServiceTests.Orchestrations.StudentEvents
{
    public partial class StudentEventOrchestrationTest
    {
        private readonly Mock<IStudentEventService> studentEventServiceMock;
        private readonly Mock<IStudentService> studentServiceMock;
        private readonly IStudentEventOrchestrationService studentEventOrchestrationService;

        public StudentEventOrchestrationTest()
        {
            this.studentEventServiceMock = new Mock<IStudentEventService>();
            this.studentServiceMock = new Mock<IStudentService>();

            this.studentEventOrchestrationService = new StudentEventOrchestrationService(
                studentEventService: this.studentEventServiceMock.Object,
                studentService: this.studentServiceMock.Object);
        }

        private static Student CreateRandomStudent() =>
            CreateRandomFiller().Create();

        private static Filler<Student> CreateRandomFiller() =>
            new Filler<Student>();
    }
}
