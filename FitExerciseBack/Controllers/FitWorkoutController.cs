using Application.FitWorkout.Boundaries;
using Application.FitWorkout.Commands.CreateExercise;
using Application.FitWorkout.Commands.EditExerciseData;
using Application.FitWorkout.Commands.EditExerciseMedia;
using Application.FitWorkout.Commands.GetExerciseById;
using Application.FitWorkout.Commands.GetExercises;
using Application.FitWorkout.Commands.GetGymExercises;
using Application.S3.Boundaries;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitExerciseBack.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class FitWorkoutController : BaseController
    {
        // private readonly IS3Service _s3Service;
        private readonly IMediatorHandler _mediatorHandler;

        public FitWorkoutController(INotificationHandler<DomainNotification> notificationHandler,
            IMediatorHandler mediatorHandler) : base(notificationHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet("GetExercises")]
        [ProducesResponseType(typeof(List<ExerciseOutput>), 200)]
        public async Task<IActionResult> GetExercises()
        {
            var userId = ObterUserId();
            var command = new GetExecisesCommand(userId);

            var response = await _mediatorHandler.SendCommand<GetExecisesCommand, List<ExerciseOutput>>(command);

            if (IsValidOperation())
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [HttpPost("UploadExercise")]
        [DisableRequestSizeLimit]
        [Authorize(Roles = "gym")]
        public async Task<IActionResult> UploadExercise([FromForm] UploadInput uploadInput)
        {
            var command = new CreateExerciseCommand(uploadInput);

            await _mediatorHandler.SendCommand<CreateExerciseCommand, bool>(command);

            if (IsValidOperation())
            {
                return Ok();
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [Authorize(Roles = "gym")]
        [HttpPut("EditExerciseData")]
        public async Task<IActionResult> EditExerciseData([FromBody] EditExerciseDataInput exerciseInput)
        {
            var command = new EditExerciseDataCommand(exerciseInput);

            await _mediatorHandler.SendCommand<EditExerciseDataCommand, bool>(command);

            if (IsValidOperation())
            {
                return Ok();
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [Authorize(Roles = "gym")]
        [HttpPut("EditExerciseMedia")]
        public async Task<IActionResult> EditExerciseMedia([FromForm] EditExerciseMediaInput input)
        {
            var command = new EditExerciseMediaCommand(input);

            await _mediatorHandler.SendCommand<EditExerciseMediaCommand, bool>(command);

            if (IsValidOperation())
            {
                return Ok();
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [HttpGet("GetExerciseById/{id}")]
        [ProducesResponseType(typeof(FullExerciseOutput), 200)]
        public async Task<IActionResult> GetExerciseById(int id)
        {
            var command = new GetExerciseByIdCommand(id);

            var response = await _mediatorHandler.SendCommand<GetExerciseByIdCommand, FullExerciseOutput>(command);

            if (IsValidOperation())
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [HttpGet("GetGymExercises")]
        [Authorize(Roles = "gym")]
        [ProducesResponseType(typeof(List<ExerciseOutput>), 200)]
        public async Task<IActionResult> GetGymExercises()
        {
            var gymId = ObterGymId();
            var command = new GetGymExecisesCommand(gymId);

            var response = await _mediatorHandler.SendCommand<GetGymExecisesCommand, List<ExerciseOutput>>(command);

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
