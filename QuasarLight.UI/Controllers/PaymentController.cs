﻿using System.Web.Mvc;
using QuasarLight.Domain.Infrastructure;
using QuasarLight.Domain.Models;

namespace QuasarLight.UI.Controllers
{
    public class PaymentController : Controller
    {
        public ActionResult PaymentRequisitesForm(string name)
        {
            return View(null, null, name);
        }

        [HttpPost]
        public object SendPaymentRequisitesToStudent(string name, string email)
        {
            /*var body = RenderToString("_PaymentRequisitesForm", name);

            dispatcher.Push(new SendMessageCommand(new Email
            {
                ToEmail = email,
                ToName = name,
                FromEmail = MailSettings.FromEmail,
                FromName = MailSettings.FromName,
                Subject = "Онлайн-школа \"Dialogue\"",
                Body = body
            }));*/
            return true;
        }
    }
}