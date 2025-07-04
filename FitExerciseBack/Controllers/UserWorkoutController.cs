﻿using Application.Gym.UseCase;
using Application.UserWorkout.Boundaries;
using Application.UserWorkout.Commands;
using Application.UserWorkout.v2.Boundaries;
using Application.UserWorkout.v2.Commands;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FitExerciseBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserWorkoutController(INotificationHandler<DomainNotification> notificationHandler,
        IMediatorHandler mediatorHandler, IGymUseCase gymUseCase) : BaseController(notificationHandler, gymUseCase)
    {
        private readonly IMediatorHandler _mediatorHandler = mediatorHandler;

        [HttpPost("AddUserWorkout")]
        public async Task<IActionResult> AddUserWorkout(AddUserWorkoutInputOld input)
        {
            if (IsGymUser())
            {
                if (!await CanOperate(input.UserId))
                {
                    return NotFound();
                }
            }
            else
            {
                input.UserId = GetUserId();
            }
            var command = new AddUserWorkoutCommandOld(input);
            await _mediatorHandler.SendCommand<AddUserWorkoutCommandOld, bool>(command);

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
        public async Task<IActionResult> GetUserExercises([FromRoute] int groupId, [FromQuery] int userId = 0)
        {
            if (IsGymUser())
            {
                if (!await CanOperate(userId))
                {
                    return NotFound();
                }
            }
            else
            {
                userId = GetUserId();
            }

            var command = new GetUserExercisesCommand(groupId, userId);

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
        public async Task<IActionResult> DeleteUserWorkout([FromRoute] int workoutId, [FromQuery] int? userId)
        {
            if (IsGymUser())
            {
                if (!await CanOperate(userId ?? 0))
                {
                    return NotFound();
                }
            }
            else
            {
                userId = GetUserId();
            }

            var command = new DeleteUserWorkoutCommand((int)userId!, workoutId);

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
        public async Task<IActionResult> UpdateUserWorkout(UpdateUserWorkoutInputOld input)
        {
            if (IsGymUser())
            {
                if (!await CanOperate(input.UserId))
                {
                    return NotFound();
                }
            }
            else
            {
                input.UserId = GetUserId();
            }
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
        // V2 //
        [HttpPost("v2/AddUserWorkout")]
        public async Task<IActionResult> AddUserWorkoutV2(AddUserWorkoutInput input)
        {
            if (IsGymUser())
            {
                if (!await CanOperate(input.UserId))
                {
                    return NotFound();
                }
            }
            else
            {
                input.UserId = GetUserId();
            }
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

        [HttpPut("v2/UpdateUserWorkouts")]
        public async Task<IActionResult> UpdateUserWorkouts(UpdateUserWorkoutsInput input)
        {
            if (IsGymUser())
            {
                if (!await CanOperate(input.UserId))
                {
                    return NotFound();
                }
            }
            else
            {
                input.UserId = GetUserId();
            }
            var command = new UpdateUserWorkoutsCommand(input);

            await _mediatorHandler.SendCommand<UpdateUserWorkoutsCommand, bool>(command);

            if (IsValidOperation())
            {
                return NoContent();
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [HttpGet("v2/GetUserWorkouts/{groupId}")]
        public async Task<IActionResult> GetUserWorkouts([FromRoute] int groupId, [FromQuery] int? userId)
        {
            if (IsGymUser())
            {
                if (!await CanOperate(userId ?? 0))
                {
                    return NotFound();
                }
            }
            else
            {
                userId = GetUserId();
            }
            var command = new GetUserWorkoutsCommand(groupId, userId ?? 0);

            var response = await _mediatorHandler.SendCommand<GetUserWorkoutsCommand, List<DynamoUserWorkoutOutput>>(command);

            if (IsValidOperation())
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [HttpPost("v2/AddCheckin")]
        [SwaggerResponse(201, Description = "Adiciona um checkin")]
        [SwaggerResponse(400, Description = "Erro", Type = typeof(List<string>))]
        public async Task<IActionResult> AddCheckin(AddCheckInWorkoutInput input)
        {
            var userId = GetUserId();

            var command = new AddCheckInWorkoutCommand(input, userId);

            await _mediatorHandler.SendCommand<AddCheckInWorkoutCommand, bool>(command);

            if (IsValidOperation())
            {
                return StatusCode(201);
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [HttpGet("v2/GetUserCheckins")]
        public async Task<IActionResult> GetUserCheckins([FromQuery] int userId = 0)
        {
            if (IsGymUser())
            {
                if (!await CanOperate(userId))
                {
                    return NotFound();
                }
            }
            else
            {
                userId = GetUserId();
            }

            var command = new ListCheckInsCommand(userId);

            var response = await _mediatorHandler.SendCommand<ListCheckInsCommand, List<CheckInWorkoutOutput>>(command);

            if (IsValidOperation())
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }
    }
}
