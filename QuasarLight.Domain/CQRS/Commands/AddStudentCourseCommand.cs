using Dialogue.Domain.Models;
using Incoding.CQRS;

namespace Dialogue.Domain.CQRS.Commands
{
    public class AddStudentCourseCommand : CommandBase
    {
        readonly StudentCourse _studentCourse;

        public AddStudentCourseCommand(StudentCourse studentCourse)
        {
            _studentCourse = studentCourse;
        }

        public override void Execute()
        {
            Repository.Save(new StudentCourse
            {
                StudentId = _studentCourse.StudentId,
                LessonId = _studentCourse.LessonId,
                Date = _studentCourse.Date
            });    
        }  
    }
}