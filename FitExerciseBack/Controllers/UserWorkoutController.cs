using Application.UserWorkout.Boundaries;
using Application.UserWorkout.Commands;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitExerciseBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserWorkoutController : BaseController
    {
        // private readonly IUserWorkoutService _userWorkoutService;
        private readonly IMediatorHandler _mediatorHandler;

        public UserWorkoutController(INotificationHandler<DomainNotification> notificationHandler,
            IMediatorHandler mediatorHandler) : base(notificationHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost("AddUserWorkout")]
        public async Task<IActionResult> AddUserWorkout(AddUserWorkoutInput input)
        {
            var command = new AddUserWorkoutCommand(input);

            await _mediatorHandler.SendCommand<AddUserWorkoutCommand, bool>(command);

            if (IsValidOperation())
            {
                return StatusCode(201);
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [HttpGet("GetUserExercises/{groupId}")]
        public async Task<IActionResult> GetExercises(int groupId)
        {
            var id = ObterUserId();

            var command = new GetUserExercisesCommand(groupId, id);

            var response = await _mediatorHandler.SendCommand<GetUserExercisesCommand, List<UserExerciseOutput>>(command);

            if (IsValidOperation())
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [HttpDelete("DeleteUserWorkout/{workoutId}")]
        public async Task<IActionResult> DeleteUserWorkout([FromRoute] int workoutId)
        {
            var userId = ObterUserId();

            var command = new DeleteUserWorkoutCommand(userId, workoutId);

            await _mediatorHandler.SendCommand<DeleteUserWorkoutCommand, bool>(command);

            if (IsValidOperation())
            {
                return NoContent();
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [HttpPut("ChangeUserWorkoutPosition")]
        public async Task<IActionResult> ChangeUserWorkoutPosition(List<UserWorkoutPositionInput> input)
        {

            var command = new ChangeUserWorkoutPositionCommand(input);

            await _mediatorHandler.SendCommand<ChangeUserWorkoutPositionCommand, bool>(command);

            if (IsValidOperation())
            {
                return NoContent();
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [HttpPut("UpdateUserWorkout")]
        public async Task<IActionResult> UpdateUserWorkout(UpdateUserWorkoutInput input)
        {
            var command = new UpdateUserWorkoutCommand(input);

            await _mediatorHandler.SendCommand<UpdateUserWorkoutCommand, bool>(command);

            if (IsValidOperation())
            {
                return NoContent();
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }
    }
}
