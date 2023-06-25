using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace EmailManager.Models.Domains
{
    public class Email
    {
        public Email()
        {
            Attachments = new Collection<Attachment>();
            Receivers = new Collection<Receiver>();
        }

        public int Id { get; set; }
        [Required]
        public string MessageSubject { get; set; }
        public string MessageBody { get; set; }
        [Required]
        public string Receiver { get; set; }
        public string ReceiverDW { get; set; }
        public int SenderId { get; set; }
        public int FooterId { get; set; }
        public int AttachementId { get; set; }
        public DateTime EmailSendDate { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
        public Sender Sender { get; set; }
        public Footer Footer { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
        public ICollection<Receiver> Receivers { get; set; }

    }
}