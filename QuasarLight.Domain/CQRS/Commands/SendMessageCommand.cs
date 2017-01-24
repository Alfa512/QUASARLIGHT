using System.Collections.Generic;
using System.Threading.Tasks;
using FluentNHibernate.Conventions;
using Incoding.CQRS;
using Mandrill;
using Mandrill.Models;
using Mandrill.Requests.Messages;
using QuasarLight.Domain.Infrastructure;
using QuasarLight.Domain.Models;

namespace QuasarLight.Domain.CQRS.Commands
{
    public class SendMessageCommand : CommandBase
    {
        readonly Email _email;

        public SendMessageCommand(Email email)
        {
            _email = email;
        }
        public override void Execute()
        {
            if (_email.FromEmail != null && _email.FromEmail.IsNotEmpty())
                _email.FromEmail = MailSettings.FromEmail;
            var to = new List<EmailAddress>
            {
                new EmailAddress
                {
                    Name = _email.ToName,
                    Email = _email.ToEmail,
                    Type = "to"
                }
            };
            SendEmails(new EmailMessage
            {
                FromEmail = _email.FromEmail,
                FromName = _email.FromName,
                To = to,
                Subject = _email.Subject,
                Html = _email.Body,
                SubAccount = MailSettings.SubAccount,
                InlineCss = true,
                AutoHtml = true
            });
        }

        public async Task<List<EmailResult>> SendEmails(EmailMessage em)
        {
            var mail = new MandrillApi(MailSettings.MandrillApi);
            return await mail.SendMessage(new SendMessageRequest(em));
        }
    }
}