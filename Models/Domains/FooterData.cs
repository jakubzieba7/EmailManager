using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmailManager.Models.Domains
{
    public class FooterData
    {
        public int Id { get; set; }
        [Required]
        public string ComplimentaryClose { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}