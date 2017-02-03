using System.ComponentModel;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using QuasarLight.Common.Contracts.Services;

namespace QuasarLight.Business.Services
{
    public class MailService : IMailService
    {
        public void SendMail(string email, string subject, string body, MailType? type = null, string emailBcc = null)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(email));
            if (!string.IsNullOrEmpty(emailBcc))
                message.Bcc.Add(new MailAddress(emailBcc));
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            var client = new SmtpClient();
            client.SendCompleted += SendCompletedCallback;

            switch (type)
            {
                case MailType.Welcome:
                    var logo = new LinkedResource(HttpContext.Current.Server.MapPath("~/img/email/logo.png"));
                    logo.ContentId = "logo";

                    var rocket = new LinkedResource(HttpContext.Current.Server.MapPath("~/img/email/rocket.png"));
                    rocket.ContentId = "rocket";


                    AlternateView avHtml = AlternateView.CreateAlternateViewFromString
                        (message.Body, null, MediaTypeNames.Text.Html);

                    avHtml.LinkedResources.Add(logo);
                    avHtml.LinkedResources.Add(rocket);
                    message.AlternateViews.Add(avHtml);


                    Attachment att = new Attachment(HttpContext.Current.Server.MapPath("~/img/email/logo.png"));
                    att.ContentDisposition.Inline = true;
                    break;
            }

            client.Send(message);
        }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            //var token = (string)e.UserState;

            if (e.Cancelled)
            {
                // Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                // Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                //Console.WriteLine("Message sent.");
            }

        }

    }
}