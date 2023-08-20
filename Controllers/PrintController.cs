using EmailManager.Models.Domains;
using EmailManager.Models.Repositories;
using Microsoft.AspNet.Identity;
using Rotativa;
using Rotativa.Options;
using System;
using System.Web.Mvc;

namespace EmailManager.Controllers
{
    public class PrintController : Controller
    {

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        private EmailRepository _emailRepository = new EmailRepository();

        public ActionResult EmailToPdf(int id)
        {
            var handle = Guid.NewGuid().ToString();
            var userId = User.Identity.GetUserId();
            var email = _emailRepository.GetEmail(id, userId);

            TempData[handle] = GetPdfContent(email);

            return Json(new
            {
                FileGuid = handle,
                FileName = $@"Email_{email.Id}.pdf"
            });
        }

        private byte[] GetPdfContent(Email email)
        {
            var pdfResult = new ViewAsPdf(@"EmailTemplate", email)
            {
                PageSize = Rotativa.Options.Size.A4,
                PageOrientation = Orientation.Portrait
            };

            return pdfResult.BuildFile(ControllerContext);
        }

        public ActionResult DownloadEmailPdf(string fileGuid, string fileName)
        {
            if (TempData[fileGuid] == null)
                throw new Exception("Błąd przy próbie eksportu emaila do PDF.");

            var data = TempData[fileGuid] as byte[];
            return File(data, "application/pdf", fileName);
        }

        public ActionResult EmailTemplate(int emailId)
        {
            var userId = User.Identity.GetUserId();
            var invoice = _emailRepository.GetEmail(emailId, userId);

            return View(invoice);
        }
    }
}