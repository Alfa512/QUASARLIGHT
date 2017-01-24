using System.Collections.Generic;
using System.Web;
using Incoding.CQRS;
using QuasarLight.Domain.Models;

namespace QuasarLight.Domain.CQRS.Commands
{
    public class AddTeacherCommand : CommandBase
    {
        readonly Teacher _teacher;

        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string ExperienceInYears { get; set; }
        public string Education { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public bool IsRussian { get; set; }
        public IEnumerable<int> Lenguages { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public HttpPostedFileBase AudioFile { get; set; }

        public AddTeacherCommand()
        {
            
        }

        public AddTeacherCommand(Teacher teacher)
        {
            _teacher = teacher;

            FullName = teacher.FullName;
            Id = teacher.Id;
            City = teacher.City;
            Description = teacher.Description;
            Education = teacher.Education;
            Email = teacher.Email;
            ExperienceInYears = teacher.ExperienceInYears;
            Description = teacher.Description;
            IsRussian = teacher.IsRussian;
        }

        public override void Execute()
        {
            Repository.Save(new Teacher
            {
                Email = _teacher.Email,
                Password = _teacher.Password,
                IsAdmin = _teacher.IsAdmin,
                FullName = _teacher.FullName,
                IsRussian = _teacher.IsRussian,
                City = _teacher.City,
                ExperienceInYears = _teacher.ExperienceInYears,
                Education = _teacher.Education,
                Description = _teacher.Description,
                ImagePath = _teacher.ImagePath,
                AudioPath = _teacher.AudioPath,
                Lenguages = _teacher.Lenguages
            });    
        }
    }
}