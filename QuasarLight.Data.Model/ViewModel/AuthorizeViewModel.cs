using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace QuasarLight.Data.Model.ViewModel
{
    public class AuthorizeViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Пожалуйста, введите логин")]
        [Display(Name = "Login")]
        [StringLength(100, ErrorMessage = "Логин должен содержать не менее 3-х символов", MinimumLength = 3)]
        public string Login { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Пожалуйста, введите имя")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Пожалуйста, введите фамилию")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Пожалуйста, введите Email")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email имеет неверный формат")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Пожалуйста, введите пароль")]
        [StringLength(100, ErrorMessage = "Длина пароля должна составлять не менее 6-ти символов", MinimumLength = 6)]
        //[Range(6, 100, ErrorMessage = "Длина пароля должна составлять не менее 6-ти символов.")]
        [DataType(DataType.Password, ErrorMessage = "Пароль должен иметь по крайней мере одну цифру")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Пароль и подтверждение не совпадают")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public ModelStateDictionary ModelState { get; set; }
    }
}