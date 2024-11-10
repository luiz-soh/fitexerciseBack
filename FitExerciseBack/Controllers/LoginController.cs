using Application.User.Boundaries.Input;
using Application.User.Commands.DeleteUser;
using Application.User.Commands.RefreshToken;
using Application.User.Commands.SignIn;
using Application.User.Commands.SignUp;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using Domain.DTOs.Token;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FitExerciseBack.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    public class LoginController(
        INotificationHandler<DomainNotification> notificationHandler,
        IMediatorHandler mediatorHandler) : BaseController(notificationHandler)
    {
        private readonly IMediatorHandler _mediatorHandler = mediatorHandler;

        [HttpPost("SignIn")]
        [SwaggerResponse(200, Description = "Sucesso", Type = typeof(TokenDto))]
        [SwaggerResponse(400, Description = "Erro", Type = typeof(List<string>))]
        public async Task<IActionResult> SignIn([FromBody] SignInInput input)
        {
            var command = new SignInCommand(input);

            var token = await _mediatorHandler.SendCommand<SignInCommand, TokenDto>(command);

            if (IsValidOperation())
            {
                return Ok(token);
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [HttpPost("SignUp")]
        [SwaggerResponse(201, Description = "Sucesso")]
        [SwaggerResponse(400, Description = "Erro", Type = typeof(List<string>))]
        public async Task<IActionResult> SignUp([FromBody] SignUpInput input)
        {
            var command = new SignUpCommand(input);

            await _mediatorHandler.SendCommand<SignUpCommand, bool>(command);

            if (IsValidOperation())
            {
                return StatusCode(201);
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [HttpDelete("DeleteUser/{userId}")]
        [SwaggerResponse(200, Description = "Sucesso")]
        [SwaggerResponse(400, Description = "Erro", Type = typeof(List<string>))]
        public async Task<IActionResult> DeleteUser([FromRoute] int userId)
        {
            var command = new DeleteUserCommand(userId);

            await _mediatorHandler.SendCommand<DeleteUserCommand, bool>(command);

            if (IsValidOperation())
            {
                return Ok();
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [HttpPost("RefreshToken")]
        [AllowAnonymous]
        [SwaggerResponse(200, Description = "Sucesso", Type = typeof(TokenDto))]
        [SwaggerResponse(400, Description = "Erro", Type = typeof(List<string>))]
        public async Task<IActionResult> RefreshToken([FromBody] UpdateTokenInput input)
        {
            var command = new RefreshTokenCommand(input);

            var token = await _mediatorHandler.SendCommand<RefreshTokenCommand, TokenDto>(command);

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
