

using Application.Hiit.Boundaries;
using Application.Hiit.Commands;
using Application.Hiit.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using MediatR;

namespace Application.Hiit.Handlers
{
    public class GetHiitByCategoryIdHandler : IRequestHandler<GetHiitByCategoryIdCommand, List<HiitOutput>>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IHiitUseCase _useCase;

        public GetHiitByCategoryIdHandler(IMediatorHandler mediatorHandler,
        IHiitUseCase useCase)
        {
            _mediatorHandler = mediatorHandler;
            _useCase = useCase;
        }
        public async Task<List<HiitOutput>> Handle(GetHiitByCategoryIdCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                var hiits = await _useCase.GetHiitByCategoryId(request.CategoryId);
                return hiits.Select(x => new HiitOutput(x)).ToList();
            }
            foreach (var error in request.ValidationResult.Errors)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, error.ErrorMessage));
            }
            return [];
        }
    }
}