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
    }
}
