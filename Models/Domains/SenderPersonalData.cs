using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailManager.Models.Domains
{
    public class SenderPersonalData
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string CompanyPositionPl { get; set; }
        public string CompanyPositionEn { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}