using System.ComponentModel.DataAnnotations;

namespace EmailManager.Models.Domains
{
    public class CompanyData
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string NIP { get; set; }
        public string REGON { get; set; }
        public string Comments { get; set; }
        [Required]
        public string Logo { get; set; }
    }
}