using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmailManager.Models.Domains
{
    public class Footer
    {
        public int Id { get; set; }
        [Required]
        public string ComplimentaryClose { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string PositionPl { get; set; }
        public string PositionEn { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public string NIP { get; set; }
        public string REGON { get; set; }
        public string Comments { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public Sender Sender { get; set; }
    }
}