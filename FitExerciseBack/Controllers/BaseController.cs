using System.Security.Claims;
using Application.Gym.UseCase;
using Domain.Base.Messages.CommonMessages.Notification;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FitExerciseBack.Controllers
{
    public abstract class BaseController(INotificationHandler<DomainNotification> notificationHandler, IGymUseCase gymUseCase) : Controller
    {
        private readonly DomainNotificationHandler _notificationHandler = (DomainNotificationHandler)notificationHandler;
        private readonly IGymUseCase _gymUseCase = gymUseCase;

        protected bool IsValidOperation()
        {
            return !_notificationHandler.HasNotification();
        }

        protected IEnumerable<string> GetMessages()
        {
            return _notificationHandler.GetNotifications().Select(x => x.Value).ToList();
        }

        protected int GetUserId()
        {
            var identifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(identifier))
                return Convert.ToInt32(identifier);

            throw new Exception("Usuário não identificado");
        }

        protected int GetGymId()
        {
            var identifier = User.FindFirstValue(ClaimTypes.Actor);
            if (!string.IsNullOrEmpty(identifier))
                return Convert.ToInt32(identifier);

            throw new Exception("Usuário não identificado");
        }

        protected bool IsGymUser()
        {
            return User.IsInRole("gym");
        }

        protected async Task<bool> CanOperate(int userId)
        {
            return await _gymUseCase.CanWorkWithUser(userId, GetGymId());
        }
    }
}
