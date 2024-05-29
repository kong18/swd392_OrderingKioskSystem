using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderingKioskSystem.Application.Business.CreateBusinessCommand;
using OrderingKioskSystem.Application.User.CreateManager;
using OrderingKioskSystem.Application.User.CreateShipper;
using System.Net.Mime;

namespace OrderingKioskSystemManagement.Api.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

       

        [HttpPost("create-manager")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<string>>> CreateManagerUser(
            [FromBody] CreateManagerUserCommand command,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(CreateManagerUser), new { id = result }, new JsonResponse<string>(result));
        }

        [HttpPost("create-shipper")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<string>>> CreateShipperUser(
            [FromBody] CreateShipperUserCommand command,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(CreateShipperUser), new { id = result }, new JsonResponse<string>(result));
        }
    }
}
