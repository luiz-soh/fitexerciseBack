using Application.UserWorkout.v2.Boundaries;
using FluentValidation;

namespace Application.UserWorkout.v2.Commands.Validation
{
    public class AddCheckInWorkoutValidation : AbstractValidator<AddCheckInWorkoutInput>
    {
        public AddCheckInWorkoutValidation()
        {
            RuleFor(x => x.Duration)
                .NotEmpty()
                .WithMessage("É obrigatório informar a duração do treino")
                .Must(BeTenMinutesOrMore)
                .WithMessage("O treino deve conter ao menos 10 minutos");

            RuleFor(x => x.GroupId)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("É obrigatório informar o grupo");
        }
        private bool BeTenMinutesOrMore(string timeString)
        {
            if (string.IsNullOrWhiteSpace(timeString))
            {
                return false;
            }

            if (DateTime.TryParseExact(timeString, "HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture,
                                       System.Globalization.DateTimeStyles.None,
                                       out DateTime parsedTime))
            {
                TimeSpan tenMinutes = new(0, 10, 0);
                return parsedTime.TimeOfDay >= tenMinutes;
            }
            return false; // Parsing failed
        }
    }
}