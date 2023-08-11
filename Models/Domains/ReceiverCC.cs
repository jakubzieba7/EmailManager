using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public int EmailId { get; set; }
        public int ReceiverDataId { get; set; }
        public ICollection<Email> Emails { get; set; }
        public ReceiverData ReceiverData { get; set; }
    }
}