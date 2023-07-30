using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmailManager.Models.Domains
{
    public class Receiver
    {
        public Receiver()
        {
            Senders = new Collection<Sender>();
            ReceivedEmails = new Collection<Email>();
            Attachments = new Collection<Attachment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public int EmailReceivedId { get; set; }
        public int SenderId { get; set; }
        public int AttachmentId { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
        public ICollection<Sender> Senders { get; set; }
        public ICollection<Email> ReceivedEmails { get; set; }

        public ICollection<Attachment> Attachments { get; set; }
    }
}