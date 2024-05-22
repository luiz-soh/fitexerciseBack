using Application.GroupWorkout.Boundaries;
using Application.GroupWorkout.Commands;
using Application.GroupWorkout.Commands.CreateGroup;
using Application.GroupWorkout.Commands.DeleteGroupById;
using Application.GroupWorkout.Commands.GetGroupById;
using Application.GroupWorkout.Commands.UpdateGroup;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FitExerciseBack.Controllers
{

    //TODO userId Deve ser pego do token e não do header
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class GroupWorkoutController : BaseController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public GroupWorkoutController(INotificationHandler<DomainNotification> notificationHandler,
        IMediatorHandler mediatorHandler) : base(notificationHandler)
        {
            _mediatorHandler = mediatorHandler;
        }


        [HttpGet("GetGroups")]
        [SwaggerResponse(200, Description = "Lista de grupos", Type = typeof(List<GroupWorkoutOutput>))]
        [SwaggerResponse(400, Description = "Erro", Type = typeof(List<string>))]
        public async Task<IActionResult> GetGroups()
        {
            var userId = Convert.ToInt32(HttpContext.Items["userId"]);

            var command = new GetGroupsCommand(userId);

            var response = await _mediatorHandler.SendCommand<GetGroupsCommand, List<GroupWorkoutOutput>>(command);

            if (IsValidOperation())
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [HttpPost("CreateGroup")]
        [SwaggerResponse(201, Description = "Cria um grupo")]
        [SwaggerResponse(400, Description = "Erro", Type = typeof(List<string>))]
        public async Task<IActionResult> CreateGroup(CreateGroupInput input)
        {
            var userId = Convert.ToInt32(HttpContext.Items["userId"]);

            var command = new CreateGroupCommand(input, userId);

            await _mediatorHandler.SendCommand<CreateGroupCommand, bool>(command);

            if (IsValidOperation())
            {
                return Created();
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [HttpPut("UpdateGroup")]
        [SwaggerResponse(200, Description = "Atualiza um grupo")]
        [SwaggerResponse(400, Description = "Erro", Type = typeof(List<string>))]
        public async Task<IActionResult> UpdateGroup(UpdateGroupInput input)
        {
            var command = new UpdateGroupCommand(input);

            await _mediatorHandler.SendCommand<UpdateGroupCommand, bool>(command);

            if (IsValidOperation())
            {
                return Ok();
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [HttpGet("GetGroupById/{id}")]
        [SwaggerResponse(200, Description = "Grupo", Type = typeof(GroupWorkoutOutput))]
        [SwaggerResponse(400, Description = "Erro", Type = typeof(List<string>))]
        public async Task<IActionResult> GetGroupById(int id)
        {
            var command = new GetGroupByIdCommand(id);

            var group = await _mediatorHandler.SendCommand<GetGroupByIdCommand, GroupWorkoutOutput>(command);

            if (IsValidOperation())
            {
                return Ok(group);
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [HttpDelete("DeleteGroupById/{id}")]
        [SwaggerResponse(200, Description = "Grupo deletado")]
        [SwaggerResponse(400, Description = "Erro", Type = typeof(List<string>))]
        public async Task<IActionResult> DeleteGroupById(int id)
        {
            var userId = Convert.ToInt32(HttpContext.Items["userId"]);

            var command = new DeleteGroupByIdCommand(id, userId);

            await _mediatorHandler.SendCommand<DeleteGroupByIdCommand, bool>(command);

            if (IsValidOperation())
            {
                return Ok();
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }
    }
}