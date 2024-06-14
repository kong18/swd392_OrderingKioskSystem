﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderingKioskSystem.Application.Business;
using OrderingKioskSystem.Application.Business.CreateBusinessCommand;
using OrderingKioskSystem.Application.Business.Delete;

using OrderingKioskSystem.Application.Business.GetBusinessById;
using OrderingKioskSystem.Application.Business.Update;
using OrderingKioskSystem.Application.Common.Pagination;
using OrderingKioskSystem.Application.Menu.Filter;
using OrderingKioskSystem.Application.Menu;
using OrderingKioskSystemManagement.Api.Controller;
using SWD.OrderingKioskSystem.Application.Payment;
using System.Net.Mime;
using OrderingKioskSystem.Application.Business.GetBusinessByFilter;

namespace OrderingKioskSystemManagement.Api.Controllers
{
    [ApiController]
    [Route("api/v1/businesses")]
    public class BusinessController : ControllerBase
    {
        private readonly ISender _mediator;

        public BusinessController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
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

        [HttpPut("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateBusiness(
            [FromRoute] string id,
            [FromBody] UpdateBusinessCommand command,
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
        public async Task<ActionResult> DeleteBusiness(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteBusinessCommand(id), cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }


        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<PagedResult<BusinessDTO>>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<PagedResult<BusinessDTO>>>> FilterBusiness(
        [FromQuery] GetBusinessByFilterQuery query,
        CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(new JsonResponse<PagedResult<BusinessDTO>>(result));
        }
        [HttpGet("{id}")]
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

        
    }
}
