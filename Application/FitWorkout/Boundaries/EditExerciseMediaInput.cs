using Microsoft.AspNetCore.Http;

namespace Application.FitWorkout.Boundaries
{
    public class EditExerciseMediaInput
    {
        public EditExerciseMediaInput()
        {
            WorkoutId = 0;
        }

        public int WorkoutId { get; set; }
        public IFormFile? Video { get; set; }
        public IFormFile? Img { get; set; }
    }
}