using Application.FitWorkout.Boundaries;
using Application.FitWorkout.Commands.GetExerciseById;
using Application.FitWorkout.Commands.GetExercises;
using Application.FitWorkout.UseCase;
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
        public async Task<IActionResult> GetExercises([FromHeader]int userId)
        {
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

        // [HttpPost("UploadExercise")]
        // [DisableRequestSizeLimit]
        // [Authorize(Roles = "gym")]
        // public async Task<IActionResult> UploadExercise([FromForm] UploadInput uploadInput)
        // {
        //     var upload = await _s3Service.UploadFile(uploadInput);

        //     if (upload)
        //     {
        //         return Ok();
        //     }
        //     else
        //     {
        //         return StatusCode(500);
        //     }
        // }

        // [Authorize(Roles = "gym")]
        // [HttpPut("EditExerciseData")]
        // public async Task<IActionResult> EditExerciseData([FromBody] EditExerciseInput exerciseInput)
        // {
        //     var response = await _s3Service.EditExerciseData(exerciseInput);

        //     if (response)
        //     {
        //         return Ok();
        //     }
        //     else
        //     {
        //         return StatusCode(500);
        //     }

        // }

        // [Authorize(Roles = "gym")]
        // [HttpPut("EditExerciseMedia")]
        // public async Task<IActionResult> EditExerciseMedia([FromForm] EditExerciseMediaInput input)
        // {
        //     var response = await _s3Service.EditExerciseMedia(input);

        //     if (response)
        //     {
        //         return Ok();
        //     }
        //     else
        //     {
        //         return StatusCode(500);
        //     }

        // }

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

    }
}
