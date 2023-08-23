using EmailManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.IO;

namespace EmailManager.Models.Repositories
{
    public class SavingAttachmentsHelper
    {
        private AttachmentRepository _attachmentRepository = new AttachmentRepository();
        private List<Attachment> attachmentsData = new List<Attachment>();
        public string attachmentDownloadFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EmailSenderApp");

        public List<string> AttachmentsFilePath(Email email, int attachmentId)
        {
            if (attachmentId == 0)
                attachmentsData = _attachmentRepository.GetAttachments(email);
            else
                attachmentsData.Add(_attachmentRepository.GetAttachment(email, attachmentId));

            List<string> attachmentsFilePaths = new List<string>();

            if (!Directory.Exists(attachmentDownloadFolderPath))
            {
                Directory.CreateDirectory(attachmentDownloadFolderPath);
            }

            foreach (var attachmentData in attachmentsData)
            {
                File.WriteAllBytes(Path.Combine(attachmentDownloadFolderPath, attachmentData.FileName + "." + attachmentData.ContentType), attachmentData.FileData);

                attachmentsFilePaths.Add(Path.Combine(attachmentDownloadFolderPath, attachmentData.FileName + "." + attachmentData.ContentType));
            }

            return attachmentsFilePaths;
        }
    }
}