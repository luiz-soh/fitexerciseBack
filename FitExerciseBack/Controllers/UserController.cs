using Application.User.Boundaries.Input;
using Application.User.Commands.AddUserEmail;
using Application.User.Commands.GetUserData;
using Application.User.Commands.GetUsersByGym;
using Application.User.Commands.RefreshToken;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using Domain.DTOs.Token;
using Domain.DTOs.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FitExerciseBack.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UserController : BaseController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public UserController(INotificationHandler<DomainNotification> notificationHandler,
            IMediatorHandler mediatorHandler) : base(notificationHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpPut("UpdateUserEmail")]
        [SwaggerResponse(200, Description = "Sucesso")]
        [SwaggerResponse(400, Description = "Erro", Type = typeof(List<string>))]
        public async Task<IActionResult> UpdateUserEmail([FromBody] AddUserEmailInput input)
        {
            var command = new AddUserEmailCommand(input);

            await _mediatorHandler.SendCommand<AddUserEmailCommand, bool>(command);

            if (IsValidOperation())
            {
                return Ok();
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [HttpGet("GetUserData/{userId}")]
        [SwaggerResponse(200, Description = "Sucesso", Type = typeof(UserDto))]
        [SwaggerResponse(400, Description = "Erro", Type = typeof(List<string>))]
        public async Task<IActionResult> GetUserData([FromRoute] int userId)
        {
            var command = new GetUserDataCommand(userId);

            var user = await _mediatorHandler.SendCommand<GetUserDataCommand, UserDto>(command);

            if (IsValidOperation())
            {
                return Ok(user);
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [HttpGet("GetUsersByGymId/{gymId}")]
        [Authorize(Roles = "gym")]
        [SwaggerResponse(200, Description = "Sucesso", Type = typeof(List<UserDto>))]
        [SwaggerResponse(400, Description = "Erro", Type = typeof(List<string>))]
        public async Task<IActionResult> GetUserByGymId([FromRoute] int gymId)
        {
            var command = new GetUsersByGymCommand(gymId);

            var users = await _mediatorHandler.SendCommand<GetUsersByGymCommand, List<UserDto>>(command);

            if (IsValidOperation())
            {
                return Ok(users);
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }
    }
}
