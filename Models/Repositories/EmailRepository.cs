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

        public Email GetEmail(int emailId, string userId)
        {
            using (var context=new ApplicationDbContext())
            {
                return context.Emails
                    .Include(x => x.Attachments)
                    .Include(x => x.Attachments.Select(y => y.FileName))
                    .Include(x => x.Attachments.Select(y => y.ContentType))
                    .Include(x => x.Senders)
                    .Include(x => x.Senders.Select(y => y.SendersPersonalData))
                    .Include(x => x.Senders.Select(y => y.SenderEmailsParams))
                    .Include(x => x.Senders.Select(y => y.SendersCompanyData))
                    .Include(x => x.User)
                    .Include(x => x.User.Address)
                    .Include(x => x.Receivers)
                    .Include(x => x.Receivers.Select(y => y.EmailAddress))
                    .Include(x => x.Receivers.Select(y => y.Name))
                    .Include(x => x.Receivers.Select(y => y.Surname))
                    .Include(x => x.Footer)
                    .Include(x => x.MessageBody)
                    .Include(x => x.MessageSubject)
                    .Single(x => x.UserId == userId && x.Id == emailId);
            }
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