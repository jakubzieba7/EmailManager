namespace EmailManager.Models.Domains
{
    public class SenderCompanyData
    {
        public int Id { get; set; }
        public int CompanyAddressId { get; set; }
        public int CompanyDataId { get; set; }

        public Address CompanyAddress { get; set; }
        public CompanyData CompanyData { get; set; }
    }
}