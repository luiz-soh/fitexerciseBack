using Application.Hiit.Boundaries;
using Application.Hiit.Commands.GetHiitByCategoryId.Validation;
using Domain.Base.Messages;

namespace Application.Hiit.Commands
{
    public class GetHiitByCategoryIdCommand : Command<List<HiitOutput>>
    {
        public GetHiitByCategoryIdCommand(int categoryId)
        {
            CategoryId = categoryId;
        }
        public int CategoryId { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new GetHiitByCategoryIdValidation().Validate(CategoryId);
            return ValidationResult.IsValid;
        }
    }
}