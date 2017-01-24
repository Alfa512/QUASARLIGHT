namespace QuasarLight.Domain.Models
{
    public class Email
    {
        public string ToEmail { set; get; }
        public string ToName { set; get; }
        public string FromEmail { set; get; } 
        public string FromName { set; get; } 
        public string Subject { set; get; } 
        public string Body { set; get; }
    }
}