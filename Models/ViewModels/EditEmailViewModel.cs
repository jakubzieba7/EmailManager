using EmailManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailManager.Models.ViewModels
{
    public class EditEmailViewModel
    {
        public Email Email { get; set; }
        public List<Receiver> Receivers { get; set; }
        public List<Footer> Footers { get; set; }
        public List<Sender> Senders { get; set; }
        public string Heading { get; set; }
    }
}