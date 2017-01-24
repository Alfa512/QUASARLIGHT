using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation.Attributes;
using Microsoft.Ajax.Utilities;
using QuasarLight.Domain.Models;
using QuasarLight.UI.Validators;

namespace QuasarLight.UI.Models
{
    [Validator(typeof(TeacherModelValidator))]
    public class TeacherVm
    {
        public TeacherVm( )
        {
            
        }
        public TeacherVm(Teacher teacher)
        {
            Id = teacher.Id;
            FullName = teacher.FullName;
            Email = teacher.Email;
            Password = teacher.Password;
            Education = teacher.Education;
            ExperienceInYears = teacher.ExperienceInYears;
            IsRussian = teacher.IsRussian;
            City = teacher.City;
            Description = teacher.Description;
            ImagePath = teacher.ImagePath;
            AudioPath = teacher.AudioPath;
            HasAudio = !teacher.AudioPath.IsNullOrWhiteSpace();
            Lenguages = teacher.Lenguages.ToList();
        }

        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsRussian { get; set; }
        public string FullName { get; set; }
        public string ExperienceInYears { get; set; }
        public string Education { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string ImagePath { get; set; }
        public string AudioPath { get; set; }
        public bool HasAudio { get; set; }
        public IList<Language> Lenguages { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public HttpPostedFileBase AudioFile { get; set; }

    }
}