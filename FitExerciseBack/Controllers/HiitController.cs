
using Application.Hiit.Boundaries;
using Application.Hiit.Commands;
using Application.Hiit.Commands.GetHiitSeriesById;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FitExerciseBack.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class HiitController : BaseController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public HiitController(INotificationHandler<DomainNotification> notificationHandler,
            IMediatorHandler mediatorHandler) : base(notificationHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet("GetHiitByCategoryId/{categoryId}")]
        [SwaggerResponse(200, Description = "Lista de hiits", Type = typeof(List<HiitOutput>))]
        [SwaggerResponse(400, Description = "Erro", Type = typeof(List<string>))]
        public async Task<IActionResult> GetHiitByCategoryId([FromRoute] int categoryId)
        {
            var command = new GetHiitByCategoryIdCommand(categoryId);

            var response = await _mediatorHandler.SendCommand<GetHiitByCategoryIdCommand, List<HiitOutput>>(command);

            if (IsValidOperation())
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [HttpGet("GetHiitSerie/{hiitId}/{take}")]
        [SwaggerResponse(200, Description = "Series do hiit", Type = typeof(List<HiitSerieOutput>))]
        [SwaggerResponse(400, Description = "Erro", Type = typeof(List<string>))]
        public async Task<IActionResult> GetHiitSerie([FromRoute] int hiitId, [FromRoute] int take)
        {

            var command = new GetHiitSeriesByIdCommand(hiitId, take);

            var response = await _mediatorHandler.SendCommand<GetHiitSeriesByIdCommand, List<HiitSerieOutput>>(command);

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
