namespace Domain.Base.Email
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(EmailMessage email);
    }
}