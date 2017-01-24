using Dialogue.Domain.Models;
using Incoding.CQRS;

namespace Dialogue.Domain.CQRS.Commands
{
    public class EditStudentLessonCommand : CommandBase
    {
        readonly StudentCourse _studentLesson;

        public EditStudentLessonCommand(StudentCourse studentLesson)
        {
            _studentLesson = studentLesson;
        }

        public EditStudentLessonCommand(StudentCourse studentLesson, Student student)
        {
            Dispatcher.Push(new EditStudentCommand(student));
            _studentLesson = studentLesson;
        }

        public override void Execute()
        {
            Repository.SaveOrUpdate(new StudentCourse
            {
                Id = _studentLesson.Id,
                StudentId = _studentLesson.StudentId,
                LessonId = _studentLesson.LessonId,
                Date = _studentLesson.Date
            });    
        }  
    }
}