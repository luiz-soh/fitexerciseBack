using Application.Hiit.Boundaries;
using Application.Hiit.Commands.GetHiitSeriesById;
using Application.Hiit.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using MediatR;

namespace Application.Hiit.Handlers
{
    public class GetHiitSeriesByIdHandler : IRequestHandler<GetHiitSeriesByIdCommand, List<HiitSerieOutput>>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IHiitUseCase _useCase;

        public GetHiitSeriesByIdHandler(IMediatorHandler mediatorHandler,
        IHiitUseCase useCase)
        {
            _mediatorHandler = mediatorHandler;
            _useCase = useCase;
        }

        public async Task<List<HiitSerieOutput>> Handle(GetHiitSeriesByIdCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                var hiits = await _useCase.GetHiitSeriesByHiitId(request.Id, request.Take);
                return hiits.Select(x => new HiitSerieOutput(x)).ToList();
            }
            foreach (var error in request.ValidationResult.Errors)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, error.ErrorMessage));
            }
            return [];
        }
    }
}