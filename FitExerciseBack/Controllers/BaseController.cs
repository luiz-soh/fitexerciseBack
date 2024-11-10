using System.Security.Claims;
using Domain.Base.Messages.CommonMessages.Notification;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FitExerciseBack.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly DomainNotificationHandler _notificationHandler;

        protected BaseController(INotificationHandler<DomainNotification> notificationHandler)
        {
            _notificationHandler = (DomainNotificationHandler)notificationHandler;
        }

        protected bool IsValidOperation()
        {
            return !_notificationHandler.HasNotification();
        }

        protected IEnumerable<string> GetMessages()
        {
            return _notificationHandler.GetNotifications().Select(x => x.Value).ToList();
        }

        protected int ObterUserId()
        {
            var identifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(identifier))
                return  Convert.ToInt32(identifier);

            throw new Exception("Usuário não identificado");
        }
    }
}
