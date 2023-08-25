using EmailManager.Models.Domains;
using EmailManager.Models.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;

namespace EmailManager.Models.Repositories
{
    public class AttachmentRepository
    {
        public List<Attachment> GetAttachments(Email email)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Attachments.Include(x => x.Email).Where(x => x.Email.UserId == email.UserId && x.EmailId == email.Id).ToList();
            }
        }

        public Attachment GetAttachment(Email email, int attachmentId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Attachments.Include(x => x.Email).Where(x => x.Email.UserId == email.UserId && x.EmailId == email.Id).Single(x=>x.Id==attachmentId);
            }
        }

        byte[] filebyte = null;

        public byte[] GetAttachmentContent(EditEmailAttachmentViewModel attachmentViewModel)
        {
            //var attachment = attachmentViewModel.InputAttachmentData;
            //var downloadFolderPath = KnownFolders.GetPath(nameof(KnownFolders.Downloads), KnownFolderFlags.DontVerify, false);

            filebyte = new byte[attachmentViewModel.InputAttachmentData.InputStream.Length];
            attachmentViewModel.InputAttachmentData.InputStream.Read(filebyte, 0, filebyte.Length);

            //save files to local disk
            //if (attachment.ContentLength > 0)
            //{
            //    var fileName = Path.GetFileName(attachment.FileName);
            //    var path = Path.Combine(downloadFolderPath, fileName);
            //    attachment.SaveAs(path);
            //}

            return filebyte;
        }
    }

}