using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Attributes;
using QuasarLight.Domain.Models;

namespace QuasarLight.UI.Models
{
    [Validator(typeof(AdminStudentVmValidator))]
    public class StudentVm
    {
        public StudentVm()
        {
            
        }

        public StudentVm(Student student)
        {
            Id = student.Id;
            Email = student.Email;
            FullName = student.FullName;
            Skype = student.Skype;
            Age = student.Age;
            TeacherId = student.Teacher?.Id;
            StudentTeacher = student.Teacher;
            StudentLanguage = student.Language;
        }

        public string Id { get; set; }
        public string TeacherId { get; set; }
        public int LenguageId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Skype { get; set; }
        public int Age { get; set; }
        public List<Lesson> StudentLessons { get; set; }
        public Teacher StudentTeacher { get; set; }
        public Language StudentLanguage { get; set; }
    }

    public class AdminStudentVmValidator : AbstractValidator<StudentVm>
    {
        public AdminStudentVmValidator()
        {
            RuleFor(m => m.FullName).NotEmpty().WithMessage("Введите имя!");
            RuleFor(m => m.Email).NotEmpty().WithMessage("Введите E-mail!");
            RuleFor(m => m.Email).EmailAddress().WithMessage("Некорректный E-mail!");
            RuleFor(m => m.Skype).NotEmpty().WithMessage("Введите Skype!");
            RuleFor(m => m.Age).NotEmpty().WithMessage("Введите возраст!");
            RuleFor(m => m.Age).LessThan(150).WithMessage("Некорректный возраст!");
            RuleFor(m => m.Age).GreaterThan(3).WithMessage("Некорректный возраст!");
        }
    }
}