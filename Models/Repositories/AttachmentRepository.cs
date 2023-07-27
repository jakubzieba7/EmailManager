using EmailManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailManager.Models.Repositories
{
    public class AttachmentRepository
    {
        public List<Attachment> GetAttachments()
        {
            throw new NotImplementedException();
        }

        public byte[] GetAttachmentContent(int id)
        {
            throw new NotImplementedException();
        }

        internal void UpdateAttachment(Attachment attachment, string userId)
        {
            throw new NotImplementedException();
        }

        internal void AddAttachment(Attachment attachment, string userId)
        {
            throw new NotImplementedException();
        }
    }
}