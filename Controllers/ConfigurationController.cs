using EmailManager.Models.Domains;
using EmailManager.Models.Repositories;
using EmailManager.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmailManager.Controllers
{
    public class ConfigurationController : Controller
    {
        private SenderRepository _senderRepository = new SenderRepository();
        private FooterRepository _footerRepository = new FooterRepository();

        // GET: Configuration
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Main() 
        {
            return View();
        }

        public ActionResult SenderPersonalDataIndex()
        {
            var userId = User.Identity.GetUserId();
            var senders = _senderRepository.GetSenders(userId);

            return View(senders);
        }

        public ActionResult SenderPersonalData(int id = 0)
        {
            var userId = User.Identity.GetUserId();
            var sender = id == 0 ? GetNewSenderPersonalData(userId) : _senderRepository.GetSenderPersonalData(id, userId);

            var vm = PrepareSenderVm(sender);

            return View(vm);
        }

        private object PrepareSenderVm(SenderPersonalData sender)
        {
            return new EditSenderPersonalDataViewModel
            {
                Heading = sender.Id == 0 ? "Nowy Nadawca" : "Nadawca",
                SenderPersonaldata = sender
            };
        }

        private SenderPersonalData GetNewSenderPersonalData(string userId)
        {
            return new SenderPersonalData
            {
                UserId = userId
            };
        }



        public ActionResult FooterDataIndex()
        {
            var userId = User.Identity.GetUserId();
            var footers = _footerRepository.GetFooters(userId);

            return View(footers);
        }

        public ActionResult FooterData(int id = 0)
        {
            var userId = User.Identity.GetUserId();
            var footer = id == 0 ? GetNewFooterData(userId) : _footerRepository.GetFooterData(id, userId);

            var vm = PrepareFooterVm(footer);

            return View(vm);
        }

        private object PrepareFooterVm(FooterData footer)
        {
            return new EditFooterDataViewModel
            {
                Heading = footer.Id == 0 ? "Nowa Stopka" : "Stopka",
                FooterData = footer
            };
        }

        private FooterData GetNewFooterData(string userId)
        {
            return new FooterData
            {
                UserId = userId
            };
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SenderPersonalData(SenderPersonalData sender)
        {
            if (!ModelState.IsValid)
            {
                var vm = PrepareSenderVm(sender);
                return View("SenderPersonaldata", vm);
            }

            if (sender.Id == 0)
                _senderRepository.Add(sender);
            else
                _senderRepository.Update(sender);

            return RedirectToAction("SenderPersonaldataIndex");
        }

        [HttpPost]
        public ActionResult DeleteSenderPersonalData(int senderId)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                _senderRepository.DeleteSenderPersonalData(senderId, userId);
            }
            catch (Exception exception)
            {
                //logowanie do pliku
                return Json(new { Success = false, Message = exception.Message });
            }

            return Json(new { Success = true });
        }

        [HttpPost]
        public ActionResult DeleteFooterData(int footerId)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                _footerRepository.DeleteFooterData(footerId, userId);
            }
            catch (Exception exception)
            {
                //logowanie do pliku
                return Json(new { Success = false, Message = exception.Message });
            }

            return Json(new { Success = true });
        }
    }
}