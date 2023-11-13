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
    [Authorize]
    public class ConfigurationController : Controller
    {
        private SenderRepository _senderRepository = new SenderRepository();
        private FooterRepository _footerRepository = new FooterRepository();
        private ReceiverRepository _receiverRepository = new ReceiverRepository();

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

        private EditSenderPersonalDataViewModel PrepareSenderVm(SenderPersonalData sender)
        {
            return new EditSenderPersonalDataViewModel
            {
                Heading = sender.Id == 0 ? "Nowy Nadawca" : "Nadawca",
                SenderPersonaldata = sender,
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

        private EditFooterDataViewModel PrepareFooterVm(FooterData footer)
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

        public ActionResult ReceiverDataIndex()
        {
            var userId = User.Identity.GetUserId();
            var receivers = _receiverRepository.GetReceivers(userId);

            return View(receivers);
        }

        public ActionResult ReceiverData(int id = 0)
        {
            var userId = User.Identity.GetUserId();
            var receiver = id == 0 ? GetNewReceiverData(userId) : _receiverRepository.GetReceiverData(id, userId);

            var vm = PrepareReceiverVm(receiver);

            return View(vm);
        }

        private EditReceiverDataViewModel PrepareReceiverVm(ReceiverData receiver)
        {
            return new EditReceiverDataViewModel
            {
                Heading = receiver.Id == 0 ? "Nowe dane Odbiorcy" : "Odbiorca",
                ReceiverData = receiver
            };
        }

        private ReceiverData GetNewReceiverData(string userId)
        {
            return new ReceiverData
            {
                UserId = userId
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReceiverData(ReceiverData receiverData)
        {
            var userId = User.Identity.GetUserId();
            receiverData.UserId = userId;

            ModelState.Remove("receiverData.UserId");

            if (!ModelState.IsValid)
            {
                var vm = PrepareReceiverVm(receiverData);
                return View("ReceiverData", vm);
            }

            if (receiverData.Id == 0)
                _receiverRepository.Add(receiverData);
            else
                _receiverRepository.Update(receiverData);

            return RedirectToAction("ReceiverDataIndex");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SenderPersonalData(SenderPersonalData senderPersonalData)
        {
            var userId = User.Identity.GetUserId();
            senderPersonalData.UserId = userId;

            ModelState.Remove("senderPersonalData.UserId");

            if (!ModelState.IsValid)
            {
                var vm = PrepareSenderVm(senderPersonalData);
                return View("SenderPersonaldata", vm);
            }

            if (senderPersonalData.Id == 0)
                _senderRepository.Add(senderPersonalData);
            else
                _senderRepository.Update(senderPersonalData);

            return RedirectToAction("SenderPersonaldataIndex");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FooterData(FooterData footerData)
        {
            var userId = User.Identity.GetUserId();
            footerData.UserId = userId;

            ModelState.Remove("footerData.UserId");

            if (!ModelState.IsValid)
            {
                var vm = PrepareFooterVm(footerData);
                return View("FooterData", vm);
            }

            if (footerData.Id == 0)
                _footerRepository.Add(footerData);
            else
                _footerRepository.Update(footerData);

            return RedirectToAction("FooterDataIndex");
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

        [HttpPost]
        public ActionResult DeleteReceiverData(int receiverId)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                _receiverRepository.DeleteReceiverData(receiverId, userId);
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