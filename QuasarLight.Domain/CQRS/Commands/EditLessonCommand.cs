using Incoding.CQRS;
using QuasarLight.Domain.Models;

namespace QuasarLight.Domain.CQRS.Commands
{
    public class EditLessonCommand : CommandBase
    {
        readonly Lesson _lesson;

        public EditLessonCommand(Lesson lesson)
        {
            _lesson = lesson;
        }

        public override void Execute()
        {
            Repository.SaveOrUpdate(new Lesson
            {
                Id = _lesson.Id,
                Student = _lesson.Student,
                Teacher = _lesson.Teacher,
                Language = _lesson.Language,
                DateTime = _lesson.DateTime,
                Comment = _lesson.Comment,
                IsTrial = _lesson.IsTrial,
            });    
        }  
    }
}