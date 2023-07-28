using EmailManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmailManager.Models.Repositories
{
    public class EmailRepository
    {
        public List<Email> GetEmails(string userId)
        {
            using (var context=new ApplicationDbContext())
            {
                return context.Emails.Include(x=>x.Sender).Where(x=>x.UserId==userId).ToList();
            }
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

        public Attachment UpdateEmailAttachment(int emailId, string userId)
        {
            throw new NotImplementedException();
        }

        public void DeleteAttachment(int emailId, string userId)
        {
            throw new NotImplementedException();
        }

        public void DeleteEmail(int emailId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}