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
        public List<Receiver> Receivers { get; set; }
        public List<FooterData> Footers { get; set; }
        public List<Sender> Senders { get; set; }
        public List<Attachment> Attachments { get; set; }
        //public List<HttpPostedFileBase> Attachments { get; set; }
        public string Heading { get; set; }
    }
}