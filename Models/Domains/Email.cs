using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailManager.Models.Domains
{
    public class Email
    {
        public Email()
        {
            Attachments = new Collection<Attachment>();
            Receivers = new Collection<Receiver>();
            Senders = new Collection<Sender>();
        }

        public int Id { get; set; }
        [Required]
        [Display(Name ="Tytuł wiadomości")]
        public string MessageSubject { get; set; }
        [Display(Name = "Treść wiadomości")]
        public string MessageBody { get; set; }
        [Required]
        [Display(Name = "Odbiorca wiadomości")]
        public string Receiver { get; set; }
        public string ReceiverDW { get; set; }
        public int SenderId { get; set; }
        public int FooterId { get; set; }
        public int AttachmentId { get; set; }
        [Display(Name = "Data wysłania wiadomości")]
        public DateTime EmailSendDate { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
        public Sender Sender { get; set; }
        [Display(Name = "Stopka")]
        public FooterData FooterData { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
        [Display(Name = "Odbiorcy wiadomości")]
        public ICollection<Receiver> Receivers { get; set; }
        public ICollection<Sender> Senders { get; set; }

    }
}