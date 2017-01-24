using Incoding.CQRS;
using QuasarLight.Domain.Models;

namespace QuasarLight.Domain.CQRS.Commands
{
    public class EditCourseCommand : CommandBase
    {
        readonly Language _course;

        public EditCourseCommand(Language course)
        {
            _course = course;
        }

        public override void Execute()
        {
            Repository.SaveOrUpdate(new Language
            {
                LanguageName = _course.LanguageName,
                IconPath = _course.IconPath
            });    
        }   
    }
}