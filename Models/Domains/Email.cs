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
            ReceiverCCs = new Collection<ReceiverCC>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Pole Tytuł jest wymagane")]
        [Display(Name = "Tytuł wiadomości")]
        public string MessageSubject { get; set; }
        [Display(Name = "Treść wiadomości")]
        public string MessageBody { get; set; }
        [Required(ErrorMessage = "Pole Adresat jest wymagane")]
        [Display(Name = "Adresat wiadomości")]
        public int ReceiverId { get; set; }
        [Display(Name = "Adresat wiadomości DW")]
        public int ReceiverCCId { get; set; }
        public int SenderId { get; set; }
        public int FooterId { get; set; }
        public int AttachmentId { get; set; }
        [Required(ErrorMessage = "Pole Data wysłania jest wymagane")]
        [Display(Name = "Data wysłania wiadomości")]
        public DateTime EmailSendDate { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
        public Receiver Receiver { get; set; }
        public ReceiverCC ReceiverCC { get; set; }
        public Sender Sender { get; set; }
        [Display(Name = "Stopka")]
        public Footer Footer { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
        public ICollection<Receiver> Receivers { get; set; }
        public ICollection<ReceiverCC> ReceiverCCs { get; set; }

    }
}