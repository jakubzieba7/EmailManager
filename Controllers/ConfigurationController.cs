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

        // GET: Configuration
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Main()
        {
            return View();
        }

        public ActionResult SenderPersonalDataInput()
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

            var vm = PrepareSenderVm(sender, userId);

            return View(vm);
        }

        private object PrepareSenderVm(SenderPersonalData sender, string userId)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SenderPersonalData(SenderPersonalData sender)
        {
            var userId = User.Identity.GetUserId();
            sender.UserId = userId;

            if (!ModelState.IsValid)
            {
                var vm = PrepareSenderVm(sender, userId);
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
    }
}