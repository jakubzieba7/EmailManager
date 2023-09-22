using EmailManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailManager.Models.ViewModels
{
    public class EditReceiverDataViewModel
    {
        public string Heading { get; set; }
        public ReceiverData ReceiverData { get; set; }
    }
}