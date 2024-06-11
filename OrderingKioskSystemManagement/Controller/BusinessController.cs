using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderingKioskSystem.Application.Business;
using OrderingKioskSystem.Application.Business.CreateBusinessCommand;
using OrderingKioskSystem.Application.Business.Delete;
using OrderingKioskSystem.Application.Business.GetAllBusiness;
using OrderingKioskSystem.Application.Business.GetBusinessById;
using OrderingKioskSystem.Application.Business.Update;
using OrderingKioskSystem.Application.Category;
using OrderingKioskSystem.Application.Category.Create;
using OrderingKioskSystem.Application.Category.Delete;
using OrderingKioskSystem.Application.Category.GetAll;
using OrderingKioskSystem.Application.Category.GetById;
using OrderingKioskSystem.Application.Category.Update;
using SWD.OrderingKioskSystem.Application.Payment;
using SWD.OrderingKioskSystem.Application.QRCode;
using System.Net.Mime;

namespace OrderingKioskSystemManagement.Api.Controller
{
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly ISender _mediator;

        public BusinessController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("business")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateBusiness(
          [FromBody] CreateBusinessCommand command,
          CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }


        [HttpPut("business")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateBusiness(
            [FromBody] UpdateBusinessCommand command,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        [HttpDelete("business/{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteBusiness(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteBusinessCommand(id), cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        [HttpGet("business")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<BusinessDTO>>> GetAllBusiness(
           CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetBusinessByFilterQuery(), cancellationToken);
            return Ok(new JsonResponse<List<BusinessDTO>>(result));
        }

        [HttpGet("business/{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BusinessDTO>> GetBusinessById(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetBusinessByIdQuery(id), cancellationToken);
            return Ok(new JsonResponse<BusinessDTO>(result));
        }

        [HttpPost("createQR")]
        public async Task<IActionResult> CreateQR([FromBody] CreateQRCommand command)
        {
            var qrCodeUrl = await _mediator.Send(command);
            return Ok(new { qrCodeUrl });
        }
    }
}
