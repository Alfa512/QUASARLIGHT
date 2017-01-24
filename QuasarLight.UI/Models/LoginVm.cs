using FluentValidation;
using FluentValidation.Attributes;

namespace QuasarLight.UI.Models
{
    [Validator(typeof(LoginVmValidator))]
    public class LoginVm
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class LoginVmValidator : AbstractValidator<LoginVm>
    {
        public LoginVmValidator()
        {
            RuleFor(m => m.Email).NotEmpty().WithMessage("Введите логин!");
            RuleFor(m => m.Password).NotEmpty().WithMessage("Введите пароль!");
        }
    }
}