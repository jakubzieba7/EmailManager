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
            SentEmails = new Collection<Email>();
            Receivers = new Collection<Receiver>();
            Footers = new Collection<Footer>();
            SenderEmailsParams = new Collection<SenderEmailParams>();
            Attachments = new Collection<Attachment>();
        }

        public int Id { get; set; }
        public int SenderPersonalDataId { get; set; }
        public int SenderCompanyDataId { get; set; }
        public int SenderEmailParamsId { get; set; }
        public int EmailId { get; set; }
        public int ReceiverId { get; set; }
        public int FooterId { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public int AttachmentId { get; set; }

        public ApplicationUser User { get; set; }
        public ICollection<SenderPersonalData> SendersPersonalData { get; set; }
        public ICollection<SenderCompanyData> SendersCompanyData { get; set; }
        public ICollection<SenderEmailParams> SenderEmailsParams { get; set; }
        public ICollection<Email> SentEmails { get; set; }
        public ICollection<Receiver> Receivers { get; set; }
        public ICollection<Footer> Footers { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
    }
}