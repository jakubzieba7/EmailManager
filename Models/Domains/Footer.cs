using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;

namespace EmailManager.Models.Domains
{
    public class Footer
    {
        public Footer()
        {
            SentEmails = new Collection<Email>();
        }

        public int Id { get; set; }
        [Required]
        public string ComplimentaryClose { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public int SenderId { get; set; }
        public int EmailId { get; set; }

        public ApplicationUser User { get; set; }

        public Sender Sender { get; set; }
        public Collection<Email> SentEmails { get; set; }
    }
}