using EmailManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailManager.Models.ViewModels
{
    public class EditSenderPersonalDataViewModel
    {
        public SenderPersonalData SenderPersonaldata { get; set; }
        public string Heading { get; set; }
    }
}