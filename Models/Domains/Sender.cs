using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            SenderEmailParamss = new Collection<SenderEmailParams>();
        }

        public int Id { get; set; }
        [Display(Name = "Nadawca wiadomości")]
        public int SenderPersonalDataId { get; set; }
        public int SenderCompanyDataId { get; set; }
        public int SenderEmailParamsId { get; set; }
        public ICollection<SenderPersonalData> SenderPersonalDatas { get; set; }
        public ICollection<SenderCompanyData> SenderCompanyDatas { get; set; }
        public ICollection<SenderEmailParams> SenderEmailParamss { get; set; }
        public ICollection<Email> Emails { get; set; }
        public SenderPersonalData SenderPersonalData { get; set; }
        public SenderCompanyData SenderCompanyData { get; set; }
        public SenderEmailParams SenderEmailParams { get; set; }
    }
}