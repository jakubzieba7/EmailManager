using EmailManager.Models.Domains;
using System.ComponentModel.DataAnnotations;

namespace EmailManager.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Adres Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi być długie na przynajmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasło i potwierdzenie hasła nie zgadzają się.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Imię i nazwisko")]
        public string Name { get; set; }
        public Address Address { get; set; }
    }
}
