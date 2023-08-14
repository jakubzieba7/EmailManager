using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmailManager.Models.Domains
{
    public class Attachment
    {
        public int Id { get; set; }
        public int Lp { get; set; }
        public int EmailId { get; set; }
        [Display(Name = "Nazwa załącznika")]
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public byte[] FileData { get; set; }
        [Display(Name = "Typ załącznika")]
        public string ContentType { get; set; }
        public Email Email { get; set; }
    }
}