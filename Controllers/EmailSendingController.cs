using EmailManager.Models.Domains;
using System;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using EmailManager.Models;
using System.IO;
using System.Linq;
using EmailManager.Models.Repositories;
using Microsoft.AspNet.Identity;

namespace EmailManager.Controllers
{
    public class EmailSendingController : Controller
    {
        private EmailRepository _emailRepository = new EmailRepository();
        private AttachmentRepository _attachmentRepository = new AttachmentRepository();
        private SmtpClient _smtp;
        private MailMessage _mail;

        private string _hostSmtp;
        private bool _enableSsl;
        private int _port;
        private string _senderEmail;
        private string _senderEmailPassword;
        private string _senderName;
        private string _filePath;
        string attachmentDownloadFolderPath;

        public async Task Send(Email email)
        {
            var emailParams = new SenderEmailParams
            {
                Id = 1,
                HostSmtp = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                SenderEmail = "services.report.new@gmail.com",
                SenderEmailPassword = "",
                SenderName = "Jakub Zięba"
            };

            _hostSmtp = emailParams.HostSmtp;
            _enableSsl = emailParams.EnableSsl;
            _port = emailParams.Port;
            _senderEmail = emailParams.SenderEmail;
            _senderEmailPassword = emailParams.SenderEmailPassword;
            _senderName = emailParams.SenderName;

            _mail = new MailMessage();
            _mail.From = new MailAddress(_senderEmail, _senderName);
            _mail.To.Add(new MailAddress(email.Receiver.ReceiverData.EmailAddress));
            _mail.IsBodyHtml = true;
            _mail.Subject = email.MessageSubject;
            _mail.BodyEncoding = Encoding.UTF8;
            _mail.SubjectEncoding = Encoding.UTF8;
            
            var attachmentsData = _attachmentRepository.GetAttachments(email).Select(x => new { x.FileData, x.FileName, x.ContentType });

            if (_attachmentRepository.GetAttachments(email).Count() > 0)
            {
                SaveFileBytesFromDBToDisk(email);

                foreach (var attachmentData in attachmentsData)
                {
                    _mail.Attachments.Add(new System.Net.Mail.Attachment(attachmentDownloadFolderPath + '\\' + attachmentData.FileName + '.' + attachmentData.ContentType));
                }
            }

            _mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(email.MessageBody, null, MediaTypeNames.Text.Plain));

            string HtmlContent = RazorViewToStringFormat.RenderRazorViewToString(this, "EmailTemplate", email);

            _mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(HtmlContent, null, MediaTypeNames.Text.Html));

            _smtp = new SmtpClient
            {
                Host = _hostSmtp,
                EnableSsl = _enableSsl,
                Port = _port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_senderEmail, _senderEmailPassword)
            };

            _smtp.SendCompleted += OnSendCompleted;

            await _smtp.SendMailAsync(_mail);
        }

        private void SaveFileBytesFromDBToDisk(Email email)
        {
            var attachmentsData = _attachmentRepository.GetAttachments(email).Select(x => new { x.FileData, x.FileName, x.ContentType });
            attachmentDownloadFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EmailSenderApp");

            if (!Directory.Exists(attachmentDownloadFolderPath))
            {
                Directory.CreateDirectory(attachmentDownloadFolderPath);
            }

            foreach (var attachmentData in attachmentsData)
            {
                System.IO.File.WriteAllBytes(Path.Combine(attachmentDownloadFolderPath, attachmentData.FileName + "." + attachmentData.ContentType), attachmentData.FileData);
            }
        }

        private void OnSendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            _smtp.Dispose();
            _mail.Dispose();
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> SendEmail(int emailId)
        {
            var userId = User.Identity.GetUserId();
            var email = _emailRepository.GetEmail(emailId, userId);

            try
            {
                await Send(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            ViewBag.SuccessfulSentMailMessage = "Mail został wysłany";

            return View("EmailTemplate", email);
        }

        public ActionResult EmailTemplate(int emailId)
        {
            var userId = User.Identity.GetUserId();

            return View(_emailRepository.GetEmail(emailId, userId));
        }
    }
}