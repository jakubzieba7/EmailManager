using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmailManager.Models.Domains
{
    public class Sender
    {
        public Sender()
        {
            SendersPersonalData = new Collection<SenderPersonalData>();
            SendersCompanyData = new Collection<SenderCompanyData>();
            SendEmails = new Collection<Email>();
            Receivers = new Collection<Receiver>();
            Footers = new Collection<Footer>();
        }

        public int Id { get; set; }
        public int SenderPersonalDataId { get; set; }
        public int SenderCompanyDataId { get; set; }
        public int EmailId { get; set; }
        public int ReceiverId { get; set; }
        public int FooterId { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<SenderPersonalData> SendersPersonalData { get; set; }
        public ICollection<SenderCompanyData> SendersCompanyData { get; set; }
        public ICollection<Email> SendEmails { get; set; }
        public ICollection<Receiver> Receivers { get; set; }
        public ICollection<Footer> Footers { get; set; }
    }
}