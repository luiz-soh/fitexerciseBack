using Application.Hiit.Boundaries;
using Application.Hiit.Commands.GetHiitSeriesById.Validation;
using Domain.Base.Messages;

namespace Application.Hiit.Commands.GetHiitSeriesById
{
    public class GetHiitSeriesByIdCommand : Command<List<HiitSerieOutput>>
    {
        public GetHiitSeriesByIdCommand(int hiitId, int take)
        {
            Id = hiitId;
            Take = take;
        }

        public int Id { get; set; }
        public int Take { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new GetHiitSeriesByIdValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}