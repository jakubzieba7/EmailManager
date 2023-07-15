using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using EmailManager.Models.Domains;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.BuilderProperties;
using Address = EmailManager.Models.Domains.Address;
using Attachment = EmailManager.Models.Domains.Attachment;

namespace EmailManager.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Emails = new Collection<Email>();
            Footers = new Collection<Footer>();
            Receivers = new Collection<Receiver>();
            Senders = new Collection<Sender>();
            Attachments = new Collection<Attachment>();
        }

        [Required]
        [Display(Name ="Nazwa użytkownika")]
        public string Name { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public ICollection<Email> Emails { get; set; }
        public ICollection<Footer> Footers { get; set; }
        public ICollection<Receiver> Receivers { get; set; }
        public ICollection<Sender> Senders { get; set; }
        public ICollection<Attachment> Attachments { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}