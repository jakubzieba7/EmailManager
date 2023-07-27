using EmailManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailManager.Models.Repositories
{
    public class EmailRepository
    {
        public List<Email> GetEmails(string userId)
        {
            throw new NotImplementedException();
        }

        public Email GetEmail(int id, string userId)
        {
            throw new NotImplementedException();
        }

        public Attachment GetAttachment(string userId)
        {
            throw new NotImplementedException();
        }

        public void Add(Email email)
        {
            throw new NotImplementedException();
        }

        public void Update(Email email)
        {
            throw new NotImplementedException();
        }
    }
}