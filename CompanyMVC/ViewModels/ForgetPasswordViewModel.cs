using System.ComponentModel.DataAnnotations;

namespace CompanyMVC.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "InValid Email")]
        public string Email { get; set; }
    }
}
