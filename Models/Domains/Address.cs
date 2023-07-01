using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailManager.Models.Domains
{
    public class Address
    {
        public Address()
        {
            Users = new Collection<ApplicationUser>();
        }

        public int Id { get; set; }
        [Required]
        [Display(Name = "Ulica")]
        public string Street { get; set; }
        [Required]
        [Display(Name = "Miejscowość")]
        public string City { get; set; }
        [Required]
        [Display(Name = "Kod pocztowy")]
        public string PostalCode { get; set; }
        [Required]
        [Display(Name = "Numer domu")]
        public string Number { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
    }
}