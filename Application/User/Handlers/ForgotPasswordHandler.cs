using System.Text;
using Application.User.Commands.AddUserEmail;
using Application.User.Commands.ForgotPassword;
using Application.User.UseCase;
using Domain.Base.Communication;
using Domain.Base.Email;
using Domain.Base.Messages.CommonMessages.Notification;
using MediatR;

namespace Application.User.Handlers
{
    public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUserUseCase _userUseCase;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordHandler(IMediatorHandler mediatorHandler,
        IEmailSender emailSender,
            IUserUseCase userUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _userUseCase = userUseCase;
            _emailSender = emailSender;
        }

        public async Task<bool> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                var user = await _userUseCase.GetRecoverCode(request.Input.Email);

                if (!string.IsNullOrEmpty(user.RecoverCode))
                {

                    var email = MontoCorpoEmail(user.RecoverCode, user.Name, user.Email);

                    var sucess = await _emailSender.SendEmailAsync(email);

                    if (sucess)
                        return sucess;
                    else
                        await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, "ocorreu um erro ao enviar o e-mail"));
                }

                return true;
            }
            foreach (var error in request.ValidationResult.Errors)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, error.ErrorMessage));
            }

            return false;
        }


        private static EmailMessage MontoCorpoEmail(string code, string username, string userEmail)
        {
            var corpoEmail = new StringBuilder();
            corpoEmail.AppendLine($"Ola: {username}. Seu codigo para recurepar a senha é:  {code}");
            corpoEmail.AppendLine("");

            var email = new EmailMessage
            {
                Subject = "Recuperação de senha",
                To = userEmail,
                Body = corpoEmail.ToString()
            };

            return email;
        }
    }
}
