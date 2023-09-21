using EmailManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailManager.Models.ViewModels
{
    public class EditFooterDataViewModel
    {
        public string Heading { get; set; }
        public FooterData FooterData { get; set; }
    }
}