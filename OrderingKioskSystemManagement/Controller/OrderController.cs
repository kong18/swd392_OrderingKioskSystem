using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderingKioskSystem.Application.Category.Create;
using OrderingKioskSystem.Application.Order.Create;
using System.Net.Mime;

namespace OrderingKioskSystemManagement.Api.Controller
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ISender _mediator;

        public OrderController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("order")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateOrder(
           [FromBody] CreateOrderCommand command,
           CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<CreateOrderResponse>(result));
        }
    }
}
