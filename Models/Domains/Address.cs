using System.ComponentModel.DataAnnotations;

namespace EmailManager.Models.Domains
{
    public class Address
    {
        public int Id { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string Number { get; set; }
    }
}