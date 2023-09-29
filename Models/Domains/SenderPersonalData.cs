using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailManager.Models.Domains
{
    public class SenderPersonalData
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Pole Nazwa jest wymagane")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Pole Stanowisko jest wymagane")]
        public string CompanyPositionPl { get; set; }
        public string CompanyPositionEn { get; set; }
        [Required(ErrorMessage = "Pole Numer telefonu jest wymagane")]
        public string PhoneNumber { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}