using EmailManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Web;
using Attachment = EmailManager.Models.Domains.Attachment;

namespace EmailManager.Models.ViewModels
{
    public class EditEmailViewModel
    {
        public Email Email { get; set; }
        public List<ReceiverData> ReceiverDatas { get; set; }
        public List<FooterData> FooterDatas { get; set; }
        public List<SenderPersonalData> SenderPersonalDatas { get; set; }
        public List<Attachment> Attachments { get; set; }
        //public List<HttpPostedFileBase> Attachments { get; set; }
        public string Heading { get; set; }
        public List<ReceiverData> ReceiverCCDatas { get; set; }
    }
}