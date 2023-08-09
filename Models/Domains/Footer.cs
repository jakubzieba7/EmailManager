using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace EmailManager.Models.Domains
{
    public class Footer
    {
        public Footer()
        {
            Emails = new Collection<Email>();
            FooterDatas = new Collection<FooterData>();
        }

        public int Id { get; set; }
        public int SenderId { get; set; }
        public int EmailId { get; set; }
        public int FooterDataId { get; set; }
        public ICollection<Email> Emails { get; set; }
        public ICollection<FooterData> FooterDatas { get; set; }
        public FooterData FooterData { get; set; }
        public Sender Sender { get; set; }
    }
}