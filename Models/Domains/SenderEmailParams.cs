using System.ComponentModel.DataAnnotations;

namespace EmailManager.Models.Domains
{
    public class SenderEmailParams
    {
        public int Id { get; set; }
        [Required]
        public string HostSmtp { get; set; }
        public bool EnableSsl { get; set; }
        public int Port { get; set; }
        [Required]
        public string SenderEmail { get; set; }
        [Required]
        public string SenderEmailPassword { get; set; }
        [Required]
        public string SenderName { get; set; }
    }
}