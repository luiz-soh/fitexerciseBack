using Application.UserWorkout.Boundaries;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using Domain.DTOs.UserWorkout;
using Domain.Entities.GroupWorkout;
using Domain.Entities.UserWorkout;

namespace Application.UserWorkout.UseCase
{
    public class UserWorkoutUseCase : IUserWorkoutUseCase
    {
        private readonly IUserWorkoutRepository _repository;
        private readonly IGroupWorkoutRepository _groupWorkoutRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public UserWorkoutUseCase(IUserWorkoutRepository repository,
                                  IGroupWorkoutRepository groupWorkoutRepository, IMediatorHandler handler)
        {
            _repository = repository;
            _groupWorkoutRepository = groupWorkoutRepository;
            _mediatorHandler = handler;
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
        {
            await _repository.UpdateUserWorkout(dto);
        }

        public async Task SaveUserWorkout(int userId, int groupId, SaveUserWorkoutDto input)
        {
            var group = await _groupWorkoutRepository.GetGroupById(groupId);
            if (group is not null && group.UserId == userId)
            {
                await _repository.SaveUserWorkout(groupId, input);
            }
            else
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("error:", "Grupo não econtrado"));
            }

        }

        public async Task UpdateUserWorkouts(int userId, int groupId, List<SaveUserWorkoutDto> input)
        {
            var group = await _groupWorkoutRepository.GetGroupById(groupId);
            if (group is not null && group.UserId == userId)
            {
                await _repository.UpdateUserWorkouts(groupId, input);
            }
            else
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("error:", "Grupo não econtrado"));
            }

        }
    }
}