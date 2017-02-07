using System.ComponentModel.DataAnnotations;

namespace QuasarLight.Data.Model.ViewModel
{
    public class ResetPasswordViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Пожалуйста, введите Email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Пожалуйста, введите пароль")]
        [StringLength(100, ErrorMessage = "Длина пароля должна составлять не менее 6-ти символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Пароль и подтверждение не совпадают")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}