using Application.UserWorkout.Boundaries;
using Domain.DTOs.UserWorkout;
using Domain.Entities.UserWorkout;

namespace Application.UserWorkout.UseCase
{
    public class UserWorkoutUseCase : IUserWorkoutUseCase
    {
        private readonly IUserWorkoutRepository _repository;

        public UserWorkoutUseCase(IUserWorkoutRepository repository)
        {
            _repository = repository;
        }

        public async Task AddUserWorkout(AddUserWorkoutDto dto)
        {
            await _repository.AddUserWorkout(dto);
        }

        public async Task DeleteUserWorkoutId(int userId, int workoutId)
        {
            await _repository.DeleteUserWorkoutId(userId, workoutId);
        }

        public async Task<List<UserExercisesDto>> GetUserExercises(int userId, int groupId)
        {
            var response = new List<UserExercisesDto>();

            var userExercises = await _repository.GetUserExercises(userId, groupId);
            if (userExercises != null)
            {
                response = userExercises;
            }

            return response;
        }

        public async Task ChangeUserWorkoutPosition(List<UserWorkoutPositionInput> input)
        {
            var workouts = input.Select(e => new ChangeUserWorkoutPositionDto(e.UserWorkoutId, e.Position)).ToList();
            await _repository.ChangeUserWorkoutPosition(workouts);
        }

        public async Task UpdateUserWorkout(UpdateUserWorkoutDto dto)
        {;
            await _repository.UpdateUserWorkout(dto);
        }
    }
}