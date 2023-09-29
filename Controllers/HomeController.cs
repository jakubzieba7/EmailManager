using EmailManager.Models.Domains;
using EmailManager.Models.Repositories;
using EmailManager.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;
using Attachment = EmailManager.Models.Domains.Attachment;

namespace EmailManager.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private EmailRepository _emailRepository = new EmailRepository();
        private SenderRepository _senderRepository = new SenderRepository();
        private ReceiverRepository _receiverRepository = new ReceiverRepository();
        private FooterRepository _footerRepository = new FooterRepository();
        private AttachmentRepository _attachmentRepository = new AttachmentRepository();

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var emails = _emailRepository.GetEmails(userId);

            return View(emails);
        }

        public ActionResult Email(int id = 0)
        {
            var userId = User.Identity.GetUserId();
            var email = id == 0 ? GetNewEmail(userId) : _emailRepository.GetEmail(id, userId);

            var vm = PrepareEmailVm(email, userId);

            return View(vm);
        }

        private EditEmailViewModel PrepareEmailVm(Email email, string userId)
        {
            return new EditEmailViewModel
            {
                Email = email,
                Heading = email.Id == 0 ? "Nowy email" : "Email",
                SenderPersonalDatas = _senderRepository.GetSenders(userId),
                FooterDatas = _footerRepository.GetFooters(userId),
                ReceiverDatas = _receiverRepository.GetReceivers(userId),
                ReceiverCCDatas = _receiverRepository.GetReceivers(userId),
            };
        }

        private Email GetNewEmail(string userId)
        {
            return new Email
            {
                UserId = userId,
                EmailSendDate = DateTime.Now
            };
        }

        public ActionResult EmailAttachment(int emailId, int attachmentId = 0)
        {
            var emailAttachment = GetNewAttachment(emailId, attachmentId);
            var vm = PrepareAttachmentVM(emailAttachment);

            return View(vm);
        }

        private EditEmailAttachmentViewModel PrepareAttachmentVM(Attachment emailAttachment)
        {
            return new EditEmailAttachmentViewModel
            {
                Attachment = emailAttachment,
                Heading = "Nowy załącznik",
            };
        }

        private Attachment GetNewAttachment(int emailId, int attachmentId)
        {
            return new Attachment
            {
                Id = attachmentId,
                EmailId = emailId
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Email(Email email)
        {
            var userId = User.Identity.GetUserId();
            email.UserId = userId;

            if (!ModelState.IsValid)
            {
                var vm = PrepareEmailVm(email, userId);
                return View("Email", vm);
            }

            if (email.Id == 0)
                _emailRepository.Add(email);
            else
                _emailRepository.Update(email);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmailAttachment(EditEmailAttachmentViewModel attachmentVM)
        {

            if (!ModelState.IsValid)
            {
                var vm = PrepareAttachmentVM(attachmentVM.Attachment);
                return View("EmailAttachment", vm);
            }

            try
            {
                var userId = User.Identity.GetUserId();
                _emailRepository.AddEmailAttachment(attachmentVM, userId);

            }
            catch (Exception)
            {

                return View("_NullReferenceExError");
            }

            return RedirectToAction("EmailAttachment", new { emailId = attachmentVM.Attachment.EmailId, attachmentId = attachmentVM.Attachment.Id });
        }

        [HttpPost]
        public ActionResult DeleteEmail(int emailId)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                _emailRepository.DeleteEmail(emailId, userId);
            }
            catch (Exception exception)
            {
                //logowanie do pliku
                return Json(new { Success = false, Message = exception.Message });
            }

            return Json(new { Success = true });
        }

        [HttpPost]
        public ActionResult DeleteAttachment(int attachmentId)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                _emailRepository.DeleteAttachment(attachmentId, userId);
            }
            catch (Exception exception)
            {
                //logowanie do pliku
                return Json(new { Success = false, Message = exception.Message });
            }

            return Json(new { Success = true, /*Email = email*/ });
        }

        [HttpPost]
        public ActionResult UpdateAttachments(int emailId)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                _emailRepository.UpdateEmailAttachments(emailId, userId);
            }
            catch (Exception exception)
            {
                //logowanie do pliku
                return Json(new { Success = false, Message = exception.Message });
            }

            return Json(new { Success = true });
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Strona o mnie";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Informacje kontaktowe";

            return View();
        }
    }
}