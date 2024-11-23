namespace Domain.Base.Email
{
    public class EmailMessage
    {
        public EmailMessage()
        {
            To = string.Empty;
            Subject = string.Empty;
            Body = string.Empty;
        }

        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}