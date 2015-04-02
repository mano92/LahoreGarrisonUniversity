using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using LahoreGarrisonUniversity.Areas.FrontEnd.Models;

namespace LahoreGarrisonUniversity.Areas.FrontEnd.Controllers
{
    public class ContactController : Controller
    {
        //
        // GET: /Contact/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendReview(ReviewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var mm = new MailMessage(model.Email, "muhammad.azeem@confiz.com")
            {
                Subject = "help...",
                Body = model.Message,
                IsBodyHtml = true
            };

            var smtp = new SmtpClient { Host = "smtp.gmail.com", EnableSsl = true };
            var NetworkCred = new System.Net.NetworkCredential();
            NetworkCred.UserName = "muhammad.yaseen@confiz.com";
            NetworkCred.Password = "dsdasdasds";
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            try
            {
                smtp.Send(mm);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            //lblMessage.Text = "Email Sent SucessFully.";
            return null;
        }
    }
}
