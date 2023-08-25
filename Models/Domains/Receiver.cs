using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace EmailManager.Models.Domains
{
    public class Receiver
    {
        public Receiver()
        {
            Emails = new Collection<Email>();
        }

        public int Id { get; set; }
        [Display(Name = "Adresat wiadomości")]
        public int ReceiverDataId { get; set; }
        public ICollection<Email> Emails { get; set; }
        public ReceiverData ReceiverData { get; set; }
    }
}