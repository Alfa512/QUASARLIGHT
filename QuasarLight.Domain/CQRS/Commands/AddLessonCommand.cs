using Incoding.CQRS;
using QuasarLight.Domain.Models;

namespace QuasarLight.Domain.CQRS.Commands
{
    public class AddLessonCommand : CommandBase
    {
        private readonly Lesson _lesson;

        public virtual string StEmail { get; set; }
        public virtual string StFullName { get; set; }
        public virtual string StSkype { get; set; }
        public virtual string StCity { get; set; }
        public virtual string StAge { get; set; }
        public virtual string LsLang { get; set; }
        public virtual string LsLevel { get; set; }
        public virtual string LsDateTime { get; set; }
        public virtual string LsComment { get; set; }
        public virtual  bool? IsIos { get; set; }

        public AddLessonCommand(Lesson lesson)
        {
            _lesson = lesson;

            LsDateTime = lesson.DateTime;
            LsComment = lesson.Comment;
        }

        public override void Execute()
        {
            Repository.Save(new Lesson
            {
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