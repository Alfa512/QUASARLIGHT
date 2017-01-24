using Incoding.CQRS;
using QuasarLight.Domain.Models;

namespace QuasarLight.Domain.CQRS.Commands
{
    public class AddStudentCommand : CommandBase
    {
        readonly Student _student;

        public AddStudentCommand(Student student)
        {
            _student = student;
        }

        public override void Execute()
        {
            Repository.Save(new Student
            {
                Teacher = _student.Teacher,
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