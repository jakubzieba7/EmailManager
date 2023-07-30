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
        public int SenderId { get; set; }
        public int EmailId { get; set; }
        public int FooterDataId { get; set; }
        public Sender Sender { get; set; }
        public Collection<Email> SentEmails { get; set; }
        public FooterData FooterData { get; set; }
    }
}