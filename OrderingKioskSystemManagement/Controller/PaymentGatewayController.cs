using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderingKioskSystem.Application.Category.Create;
using OrderingKioskSystem.Application.Category.Delete;
using OrderingKioskSystem.Application.Category.GetAll;
using OrderingKioskSystem.Application.Category.GetById;
using OrderingKioskSystem.Application.Category.Update;
using OrderingKioskSystem.Application.Category;
using OrderingKioskSystemManagement.Api.Controller;
using System.Net.Mime;
using OrderingKioskSystem.Application.Business.GetBusinessByFilter;
using OrderingKioskSystem.Application.Business;
using OrderingKioskSystem.Application.Common.Pagination;
using SWD.OrderingKioskSystem.Application.PaymentGateway.Create;
using SWD.OrderingKioskSystem.Application.PaymentGateway.Update;
using SWD.OrderingKioskSystem.Application.PaymentGateway.Delete;
using SWD.OrderingKioskSystem.Application.PaymentGateway;
using SWD.OrderingKioskSystem.Application.PaymentGateway.Filter;

namespace SWD.OrderingKioskSystemManagement.Api.Controller
{
    [ApiController]
    [Route("api/v1/payment-gateway")]
    public class PaymentGatewayController : ControllerBase
    {
        private readonly ISender _mediator;

        public PaymentGatewayController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreatePaymentGateway(
           [FromForm] CreatePaymentGatewayCommand command,
           CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdatePaymentGateway(
            [FromForm] UpdatePaymentGatewayCommand command,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeletePaymentGateway(
            [FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeletePaymentGatewayCommand(id), cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<PagedResult<BusinessDTO>>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<PagedResult<PaymentGatewayDTO>>>> FilterPaymentGateway(
            [FromQuery] FilterPaymentGatewayQuery query,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(new JsonResponse<PagedResult<PaymentGatewayDTO>>(result));
        }
    }
}
