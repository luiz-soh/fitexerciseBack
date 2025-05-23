using Application.Gym.Boundaries;
using Application.Gym.Commands.CreateGym;
using Application.Gym.Commands.LogIn;
using Application.Gym.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using Domain.DTOs.Gym;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FitExerciseBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "gym")]
    public class GymController(INotificationHandler<DomainNotification> notificationHandler,
        IMediatorHandler mediatorHandler, IGymUseCase gymUseCase) : BaseController(notificationHandler, gymUseCase)
    {
        private readonly IMediatorHandler _mediatorHandler = mediatorHandler;

        [HttpPost("CreateGym")]
        [AllowAnonymous]
        [SwaggerResponse(201, Description = "Sucesso")]
        [SwaggerResponse(400, Description = "Erro", Type = typeof(List<string>))]
        public async Task<IActionResult> UpdateUserEmail([FromBody] CreateGymInput input)
        {
            var command = new CreateGymCommand(input);

            await _mediatorHandler.SendCommand<CreateGymCommand, bool>(command);

            if (IsValidOperation())
            {
                return StatusCode(201);
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [HttpPost("GetGymToken")]
        [AllowAnonymous]
        [SwaggerResponse(200, Description = "Sucesso", Type = typeof(GymTokenDto))]
        [SwaggerResponse(400, Description = "Erro", Type = typeof(List<string>))]
        public async Task<IActionResult> GetGymToken([FromBody] LoginInput input)
        {
            var command = new LogInCommand(input);

            var token = await _mediatorHandler.SendCommand<LogInCommand, GymTokenDto>(command);

            if (IsValidOperation())
            {
                return Ok(token);
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }
    }
}
