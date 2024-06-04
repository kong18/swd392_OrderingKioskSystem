using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using OrderingKioskSystem.Application.ProductMenu.Create;
using OrderingKioskSystem.Application.ProductMenu.Update;
using OrderingKioskSystem.Application.ProductMenu.Delete;

namespace OrderingKioskSystemManagement.Api.Controller
{
    [ApiController]
    public class ProductMenuController : ControllerBase
    {
        private readonly ISender _mediator;

        public ProductMenuController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("product-menu")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateProductMenu(
           [FromBody] CreateProductMenuCommand command,
           CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        [HttpPut("product-menu")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateProductMenu(
            [FromBody] UpdateProductMenuCommand command,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        [HttpDelete("product-menu/{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteProductMenu(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteProductMenuCommand(id), cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

    }
}
