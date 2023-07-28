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
            using (var context = new ApplicationDbContext())
            {
                return context.Emails.Include(x => x.Sender).Where(x => x.UserId == userId).ToList();
            }
        }

        public Email GetEmail(int emailId, string userId)
        {
            using (var context = new ApplicationDbContext())
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

        public Attachment GetAttachment(string userId, int attachmentId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Attachments
                    .Include(x => x.Email)
                    .Single(x => x.Email.UserId == userId && x.Email.AttachmentId == attachmentId);
            }
        }

        public void Add(Email email)
        {
            using (var context = new ApplicationDbContext())
            {
                email.EmailSendDate = DateTime.Now;
                context.Emails.Add(email);
                context.SaveChanges();
            }
        }

        public void Update(Email email)
        {
            using (var context=new ApplicationDbContext())
            {
                var emailToUpdate = context.Emails.Single(x => x.Id == email.Id);

                emailToUpdate.MessageSubject=email.MessageSubject;
                emailToUpdate.MessageBody=email.MessageBody;
                emailToUpdate.Footer = email.Footer;
                emailToUpdate.Receiver = email.Receiver;
                emailToUpdate.EmailSendDate=DateTime.Now;

                context.SaveChanges();
            }
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