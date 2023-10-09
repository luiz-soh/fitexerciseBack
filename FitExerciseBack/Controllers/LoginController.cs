using Application.User.Boundaries.Input;
using Application.User.Commands.SignIn;
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
    public class LoginController : BaseController
    {
        private readonly IMediatorHandler _mediatorHandler;
        public LoginController(INotificationHandler<DomainNotification> notificationHandler,
            IMediatorHandler mediatorHandler) : base(notificationHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

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
    }
}
