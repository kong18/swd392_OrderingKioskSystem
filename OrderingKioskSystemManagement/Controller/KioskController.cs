using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderingKioskSystem.Application.Category.Create;
using OrderingKioskSystem.Application.Category.Delete;
using OrderingKioskSystem.Application.Category.GetAll;
using OrderingKioskSystem.Application.Category.GetById;
using OrderingKioskSystem.Application.Category.Update;
using OrderingKioskSystem.Application.Category;
using System.Net.Mime;
using OrderingKioskSystem.Application.Kiosk.Create;
using OrderingKioskSystem.Application.Kiosk.Update;
using OrderingKioskSystem.Application.Kiosk.Delete;
using OrderingKioskSystem.Application.Kiosk;
using OrderingKioskSystem.Application.Kiosk.GetById;
using OrderingKioskSystem.Application.Kiosk.GetAll;

namespace OrderingKioskSystemManagement.Api.Controller
{
    [ApiController]
    public class KioskController : ControllerBase
    {
        private readonly ISender _mediator;

        public KioskController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("kiosk")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateKiosk(
           [FromBody] CreateKioskCommand command,
           CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        [HttpPut("kiosk")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateKiosk(
            [FromBody] UpdateKioskCommand command,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        [HttpDelete("kiosk/{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteKiosk(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteKioskCommand(id), cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        [HttpGet("kiosk")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<KioskDTO>>> GetAllKiosk(
           CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetKioskQuery(), cancellationToken);
            return Ok(new JsonResponse<List<KioskDTO>>(result));
        }

        [HttpGet("kiosk/{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<KioskDTO>> GetAllKiosk(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetKioskByIdQuery(id), cancellationToken);
            return Ok(new JsonResponse<KioskDTO>(result));
        }
    }
}
