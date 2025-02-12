using Application.Gym.Boundaries;
using Application.User.Boundaries.Input;
using Application.User.Boundaries.Output;
using Application.User.Commands.AddUserEmail;
using Application.User.Commands.GetUserData;
using Application.User.Commands.GetUsersByGym;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
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
        [SwaggerResponse(200, Description = "Sucesso", Type = typeof(PaginatedUsersOutput))]
        [SwaggerResponse(400, Description = "Erro", Type = typeof(List<string>))]
        public async Task<IActionResult> GetUserByGymId([FromQuery] int? perPage, [FromQuery] int? page, [FromQuery] string orderby, [FromQuery] string order, [FromQuery] string? search)
        {
            var gymId = ObterGymId();
            var input = new PaginatedInput(gymId, perPage, page, orderby, order, search);
            var command = new GetUsersByGymCommand(input);

            var users = await _mediatorHandler.SendCommand<GetUsersByGymCommand, PaginatedUsersOutput>(command);

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
