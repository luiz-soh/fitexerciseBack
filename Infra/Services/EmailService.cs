using Domain.Base.Email;
using Domain.Configuration;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;


namespace LaBarber.Infra.Services
{
    public class EmailSender(IOptions<Secrets> secrets) : IEmailSender
    {
        private readonly string _sendGridApiKey = secrets.Value.SendGridApiKey;
        public Secrets Secrets { get; } = secrets.Value;

        public async Task<bool> SendEmailAsync(EmailMessage email)
        {
            var client = new SendGridClient(_sendGridApiKey);
            var to = new EmailAddress(email.To);
            var from = new EmailAddress
            {
                Email = Secrets.FromAddress,
                Name = Secrets.FromName
            };

            var message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);
            var response = await client.SendEmailAsync(message);

            return response.IsSuccessStatusCode;
        }
    }
}