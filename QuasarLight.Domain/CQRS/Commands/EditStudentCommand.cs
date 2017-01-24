using Incoding.CQRS;
using QuasarLight.Domain.Models;

namespace QuasarLight.Domain.CQRS.Commands
{
    public class EditStudentCommand : CommandBase
    {
        readonly Student _student;

        public EditStudentCommand(Student student)
        {
            _student = student;
        }

        public override void Execute()
        {
            Repository.SaveOrUpdate(new Student
            {
                Id = _student.Id,
                Teacher= _student.Teacher,
                Email = _student.Email,
                FullName = _student.FullName,
                Skype = _student.Skype,
                City = _student.City,
                Age = _student.Age,
                Language = _student.Language
            });    
        }   
    }
}