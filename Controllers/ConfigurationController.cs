using EmailManager.Models.Repositories;
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

        public ActionResult SenderPersonalData() 
        {
            var userId = User.Identity.GetUserId();
            var senders = _senderRepository.GetSenders(userId);

            return View(senders);
        }



        [HttpPost]
        public ActionResult DeleteEmail(int senderId)
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