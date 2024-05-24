using Application.Hiit.Boundaries;
using FluentValidation;

namespace Application.Hiit.Commands.GetHiitSeriesById.Validation
{
    public class GetHiitSeriesByIdValidation : AbstractValidator<GetHiitSeriesByIdCommand>
    {
        public GetHiitSeriesByIdValidation()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Hiit Id é obrigatório");

            RuleFor(x => x.Take)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("a quantidade é obrigatório");
        }
    }
}