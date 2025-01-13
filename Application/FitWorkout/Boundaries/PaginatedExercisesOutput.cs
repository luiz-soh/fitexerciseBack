using Domain.Base.Paginated;

namespace Application.FitWorkout.Boundaries
{
    public class PaginatedExercisesOutput
    {
        public PaginatedExercisesOutput()
        {
            Total = 0;
            Exercises = [];
        }

        public PaginatedExercisesOutput(PaginatedExercisesDto dto)
        {
            Total = dto.Total;
            Exercises = [.. dto.Exercises.Select(x => new ExerciseOutput(x))];
        }

        public int Total { get; set; }
        public List<ExerciseOutput> Exercises { get; set; }
    }
}