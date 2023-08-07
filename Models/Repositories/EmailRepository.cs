using EmailManager.Models.Domains;
using EmailManager.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Attachment = EmailManager.Models.Domains.Attachment;

namespace EmailManager.Models.Repositories
{
    public class EmailRepository
    {
        private AttachmentRepository _attachmentRepository = new AttachmentRepository();

        public List<Email> GetEmails(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Emails.Include(x => x.Senders).Where(x => x.UserId == userId).ToList();
            }
        }

        public Email GetEmail(int emailId, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Emails
                    .Include(x => x.Attachments)
                    .Include(x => x.Senders)
                    .Include(x => x.User)
                    .Include(x => x.User.Address)
                    .Include(x => x.Receivers)
                    .Include(x => x.Footers)
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
            using (var context = new ApplicationDbContext())
            {
                var emailToUpdate = context.Emails.Single(x => x.Id == email.Id && x.UserId == email.UserId);

                emailToUpdate.SenderId = email.SenderId;
                emailToUpdate.MessageSubject = email.MessageSubject;
                emailToUpdate.MessageBody = email.MessageBody;
                emailToUpdate.FooterId = email.FooterId;
                emailToUpdate.ReceiverId = email.ReceiverId;
                emailToUpdate.EmailSendDate = DateTime.Now;

                context.SaveChanges();
            }
        }

        public void AddEmailAttachment(EditEmailAttachmentViewModel attachmentVM, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                //weryfikacja czy dany rekord istnieje w bazie danych
                var email = context.Emails.Single(x => x.Id == attachmentVM.Attachment.EmailId && x.UserId == userId);
                //var attachmentLp = email.Attachments.Select(x => x.Lp).Last();

                //attachmentVM.Attachment.Lp = attachmentLp + 1;
                attachmentVM.Attachment.FileData = _attachmentRepository.GetAttachmentContent(attachmentVM);
                attachmentVM.Attachment.FileName = GetFileData.FileNameFromStreamFileName(attachmentVM.InputAttachmentData.FileName);
                attachmentVM.Attachment.ContentType = GetFileData.FileExtension(attachmentVM.InputAttachmentData.FileName);

                context.Attachments.Add(attachmentVM.Attachment);
                context.SaveChanges();
            }
        }

        public void UpdateEmailAttachment(Attachment attachment, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var attachmentToUpdate = context.Attachments.Include(x => x.Email).Include(x => x.FileData).Single(x => x.Id == attachment.Id && x.EmailId == attachment.EmailId && x.Email.UserId == userId);

                attachmentToUpdate.Lp = attachment.Lp;
                attachmentToUpdate.ContentType = attachment.ContentType;
                attachmentToUpdate.FileName = attachment.FileName;
                attachmentToUpdate.FileData = attachment.FileData;
            }
        }

        public void DeleteAttachment(int emailId, int attachmentId, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var attachmentToDelete = context.Attachments.Single(x => x.EmailId == emailId && x.Email.AttachmentId == attachmentId && x.Email.UserId == userId);

                context.Attachments.Remove(attachmentToDelete);
                context.SaveChanges();
            }
        }

        public void DeleteEmail(int emailId, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var emailToDelete = context.Emails.Single(x => x.Id == emailId && x.UserId == userId);

                context.Emails.Remove(emailToDelete);
                context.SaveChanges();
            }
        }
    }
}