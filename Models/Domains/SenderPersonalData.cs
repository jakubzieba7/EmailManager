using System.ComponentModel.DataAnnotations;

namespace EmailManager.Models.Domains
{
    public class SenderPersonalData
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string CompanyPositionPl { get; set; }
        public string CompanyPositionEn { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}