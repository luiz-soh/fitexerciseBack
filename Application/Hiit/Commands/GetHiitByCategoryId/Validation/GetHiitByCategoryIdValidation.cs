using FluentValidation;

namespace Application.Hiit.Commands.GetHiitByCategoryId.Validation
{
    public class GetHiitByCategoryIdValidation : AbstractValidator<int>
    {
        public GetHiitByCategoryIdValidation()
        {
            RuleFor(x => x)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Id da categoria é obrigatório");
        }
    }
}