using System.ComponentModel.DataAnnotations;

namespace Company.G03.PL.ViewModels.Auth
{
    public class ResetPasswordViewModel
    {

        [Required(ErrorMessage = "Password is Required !!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "ConfirmPassword is Required !!")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "ConfirmPassword does not match Password")]
        public string ConfirmPassword { get; set; }

    }
}
