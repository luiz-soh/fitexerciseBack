using Domain.DTOs.FitWorkout;

namespace Domain.Base.Paginated
{
        public class PaginatedExercisesDto
    {
        public PaginatedExercisesDto()
        {
            Total = 0;
            Exercises = [];
        }

        public PaginatedExercisesDto(int total, List<GetExercisesDto>? exercises)
        {
            Total = total;
            Exercises = exercises ?? [];
        }

        public int Total { get; set; }
        public List<GetExercisesDto> Exercises { get; set; }
    }
}