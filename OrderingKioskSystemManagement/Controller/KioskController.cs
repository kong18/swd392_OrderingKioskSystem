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
using OrderingKioskSystem.Application.Common.Pagination;
using OrderingKioskSystem.Application.Kiosk.Get;
using SWD.OrderingKioskSystem.Domain.Repositories;

namespace OrderingKioskSystemManagement.Api.Controller
{
    [ApiController]
    [Route("api/v1/kiosks")]
    public class KioskController : ControllerBase
    {
        private readonly ISender _mediator;

        public KioskController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
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

        [HttpPut("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateKiosk(
            [FromRoute] string id,
            [FromBody] UpdateKioskCommand command,
            CancellationToken cancellationToken = default)
        {
            command.Id = id; // Ensure the command has the id
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        [HttpDelete("{id}")]
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

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<PagedResult<KioskDTO>>> SearchKiosks(
            [FromQuery] string? location,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = PaginationDefaults.DefaultPageSize,
            CancellationToken cancellationToken = default)
        {
            var query = new SearchKioskQuery
            {
                Location = location,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query, cancellationToken);
            return Ok(new JsonResponse<PagedResult<KioskDTO>>(result));
        }

        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<KioskDTO>> GetKioskById(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetKioskByIdQuery(id), cancellationToken);
            return Ok(new JsonResponse<KioskDTO>(result));
        }
    }
}
