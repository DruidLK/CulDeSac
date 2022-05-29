using System;
using System.Threading.Tasks;
using CulDeSac.Models;

namespace CulDeSac.Services.StudentEvents
{
    public interface IStudentEventService
    {
        void ListenToStudentEvent(Func<Student, ValueTask> studentEventHandler);
    }
}
