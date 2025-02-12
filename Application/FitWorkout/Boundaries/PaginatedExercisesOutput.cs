using Domain.Base.Paginated;
using Domain.DTOs.FitWorkout;

namespace Application.FitWorkout.Boundaries
{
    public class PaginatedExercisesOutput
    {
        public PaginatedExercisesOutput()
        {
            Total = 0;
            Exercises = [];
        }

        public PaginatedExercisesOutput(PaginatedDto<GetExercisesDto> dto)
        {
            Total = dto.Total;
            Exercises = [.. dto.ObjectDto.Select(x => new ExerciseOutput(x))];
        }

        public int Total { get; set; }
        public List<ExerciseOutput> Exercises { get; set; }
    }
}