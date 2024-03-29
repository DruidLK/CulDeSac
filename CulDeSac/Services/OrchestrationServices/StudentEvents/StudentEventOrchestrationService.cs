﻿using CulDeSac.Services.StudentEvents;
using CulDeSac.Services.StudentServices;

namespace CulDeSac.Services.OrchestrationServices.StudentEvents
{
    public class StudentEventOrchestrationService : IStudentEventOrchestrationService
    {
        private readonly IStudentEventService studentEventService;
        private readonly IStudentService studentService;

        public StudentEventOrchestrationService(
            IStudentEventService studentEventService,
            IStudentService studentService)
        {
            this.studentEventService = studentEventService;
            this.studentService = studentService;
        }

        public void ListenToStudentEvents()
        {
            this.studentEventService.ListenToStudentEvent(async (student) =>
            {
                await this.studentService.AddStudentAsync(student);
            });
        }
    }
}
