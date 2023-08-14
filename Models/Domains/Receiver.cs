using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace EmailManager.Models.Domains
{
    public class Receiver
    {
        public Receiver()
        {
            //Senders = new Collection<Sender>();
            Emails = new Collection<Email>();
            //Attachments = new Collection<Attachment>();
        }

        public int Id { get; set; }
        //public int SenderId { get; set; }
        //public int EmailId { get; set; }
        //public int AttachmentId { get; set; }
        [Display(Name = "Adresat wiadomości")]
        public int ReceiverDataId { get; set; }
        //public ICollection<Sender> Senders { get; set; }
        public ICollection<Email> Emails { get; set; }
        //public ICollection<Attachment> Attachments { get; set; }
        public ReceiverData ReceiverData { get; set; }
    }
}