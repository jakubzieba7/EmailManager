using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmailManager.Models.Domains
{
    public class ReceiverCC
    {
        public ReceiverCC()
        {
            Emails = new Collection<Email>();
        }

        public int Id { get; set; }
        [Display(Name = "Adresat DW wiadomości")]
        public Nullable<int> ReceiverDataId { get; set; }
        public ICollection<Email> Emails { get; set; }
        public ReceiverData ReceiverData { get; set; }
    }
}