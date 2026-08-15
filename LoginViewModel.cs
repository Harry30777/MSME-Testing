using System.ComponentModel.DataAnnotations;

namespace MsmePortal.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "कृपया लॉगिन आईडी दर्ज करें (Please enter Login ID)")]
        [Display(Name = "लॉगिन आईडी (Login ID)")]
        public string LoginId { get; set; } = string.Empty;

        [Required(ErrorMessage = "कृपया पासवर्ड दर्ज करें (Please enter Password)")]
        [DataType(DataType.Password)]
        [Display(Name = "पासवर्ड (Password)")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "मुझे याद रखें")]
        public bool RememberMe { get; set; }
    }
}
