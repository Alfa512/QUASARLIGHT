namespace QuasarLight.Common.Contracts.Services
{
    public interface IMailService
    {
        void SendMail(string email, string subject, string body, MailType? type = null, string emailBcc = null);
    }

    public enum MailType
    {
        Welcome
    }
}