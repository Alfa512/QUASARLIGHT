using FluentValidation;
using QuasarLight.UI.Models;

namespace QuasarLight.UI.Validators
{
    /*public class TeacherModelValidator : AbstractValidator<TeacherVm>
    {
        public TeacherModelValidator()
        {
            RuleFor(m => m.FullName).NotEmpty().WithMessage("Введите имя!");
            RuleFor(m => m.FullName).Length(2, 255).WithMessage("Длина имени от 2-х до 255 символов!");
            RuleFor(m => m.Email).NotEmpty().WithMessage("Введите E-Mail!");
            RuleFor(m => m.Email).EmailAddress().WithMessage("Некорректный E-Mail!");
            RuleFor(m => m.ExperienceInYears).NotEmpty().WithMessage("Укажите стаж!");
            RuleFor(m => m.Description).NotEmpty().WithMessage("Введите описание!");
        }
    }*/
}