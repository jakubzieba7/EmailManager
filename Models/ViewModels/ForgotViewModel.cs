using System.ComponentModel.DataAnnotations;

namespace EmailManager.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
