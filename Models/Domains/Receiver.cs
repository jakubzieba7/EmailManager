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
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        public int EmailSendId { get; set; }
        public int SenderId { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<Sender> Senders { get; set; }
        public ICollection<Email> ReceivedEmails { get; set; }
    }
}