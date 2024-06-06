using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderingKioskSystem.Application.Category;
using OrderingKioskSystem.Application.Common.Models;
using OrderingKioskSystem.Application.DashboardCategory.GetPopularCategories;
using OrderingKioskSystem.Application.DashboardCategory;
using OrderingKioskSystem.Application.Overview;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using OrderingKioskSystemManagement.Api.Controller;

namespace OrderingKioskSystemManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashBoardController : ControllerBase
    {
        private readonly ISender _mediator;

        public DashBoardController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("overview")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<JsonResponse<List<OverviewDTO>>>> GetOverview(CancellationToken cancellationToken)
        {
            var overviewData = await _mediator.Send(new GetOverviewQuery(), cancellationToken);
            return Ok(new JsonResponse<List<OverviewDTO>>(overviewData));
        }

        [HttpGet("popular")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<JsonResponse<List<PopularCategoryDTO>>>> GetPopularCategories(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetPopularCategoriesQuery(), cancellationToken);
            return Ok(new JsonResponse<List<PopularCategoryDTO>>(result));
        }
    }
}
