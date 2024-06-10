using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderingKioskSystem.Application.Business;
using OrderingKioskSystem.Application.Business.CreateBusinessCommand;
using OrderingKioskSystem.Application.Business.Delete;
using OrderingKioskSystem.Application.Business.GetAllBusiness;
using OrderingKioskSystem.Application.Business.GetBusinessById;
using OrderingKioskSystem.Application.Business.Update;
using OrderingKioskSystem.Application.Shipper;
using OrderingKioskSystem.Application.Shipper.CreateShipper;
using OrderingKioskSystem.Application.Shipper.DeleteShipper;
using OrderingKioskSystem.Application.Shipper.GetAllShipper;
using OrderingKioskSystem.Application.Shipper.GetShipperById;
using OrderingKioskSystem.Application.Shipper.UpdateShipper;
using System.Net.Mime;

namespace OrderingKioskSystemManagement.Api.Controller
{
    [ApiController]
    public class ShipperController : ControllerBase
    { 
            private readonly ISender _mediator;

            public ShipperController(ISender mediator)
            {
                _mediator = mediator;
            }

        [HttpPost("shipper")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateShipper(
          [FromBody] CreateShipperCommand command,
          CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        [HttpPut("shipper")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateShipper(
           [FromBody] UpdateShipperCommand command,
           CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        [HttpDelete("shipper/{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteShipper(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteShipperCommand(id), cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        [HttpGet("shipper")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ShipperDTO>>> GetAllShipper(
           CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetShipper(), cancellationToken);
            return Ok(new JsonResponse<List<ShipperDTO>>(result));
        }

        [HttpGet("shipper/{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ShipperDTO>> GeShipperById(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetShipperByIdQuery(id), cancellationToken);
            return Ok(new JsonResponse<ShipperDTO>(result));
        }
    }
}
