using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmailManager.Models.Domains
{
    public class Sender
    {
        public Sender()
        {
            SenderPersonalDatas = new Collection<SenderPersonalData>();
            SenderCompanyDatas = new Collection<SenderCompanyData>();
            Emails = new Collection<Email>();
            //Receivers = new Collection<Receiver>();
            //Footers = new Collection<Footer>();
            SenderEmailParamss = new Collection<SenderEmailParams>();
            //Attachments = new Collection<Attachment>();
        }

        public int Id { get; set; }
        public int SenderPersonalDataId { get; set; }
        public int SenderCompanyDataId { get; set; }
        public int SenderEmailParamsId { get; set; }
        //public int EmailId { get; set; }
        //public int ReceiverId { get; set; }
        //public int FooterId { get; set; }
        //public int AttachmentId { get; set; }
        public ICollection<SenderPersonalData> SenderPersonalDatas { get; set; }
        public ICollection<SenderCompanyData> SenderCompanyDatas { get; set; }
        public ICollection<SenderEmailParams> SenderEmailParamss { get; set; }
        public ICollection<Email> Emails { get; set; }
        //public ICollection<Receiver> Receivers { get; set; }
        //public ICollection<Footer> Footers { get; set; }
        //public ICollection<Attachment> Attachments { get; set; }
        public SenderPersonalData SenderPersonalData { get; set; }
    }
}