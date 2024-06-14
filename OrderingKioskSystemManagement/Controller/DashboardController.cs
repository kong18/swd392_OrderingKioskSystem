using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderingKioskSystemManagement.Api.Controller;
using SWD.OrderingKioskSystem.Application.Dashboard;
using System.Net.Mime;

[ApiController]
[Route("api/v1/sales")]
public class SalesController : ControllerBase
{
    private readonly ISender _mediator;

    public SalesController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<JsonResponse<SalesDataDTO>>> GetSalesData(CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetSalesDataQuery(), cancellationToken);
        return Ok(new JsonResponse<SalesDataDTO>(result));
    }
}
