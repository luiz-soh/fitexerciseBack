using Domain.Base.Messages;
using Domain.Base.Messages.CommonMessages.Notification;

namespace Domain.Base.Communication
{
    public interface IMediatorHandler
    {
        Task<TResponse> SendCommand<TCommand, TResponse>(TCommand command) where TCommand : Command<TResponse>;
        Task PublishNotification<T>(T notification) where T : DomainNotification;
    }
}
