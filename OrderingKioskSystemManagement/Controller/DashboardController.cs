using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderingKioskSystem.Application.Category;
using OrderingKioskSystem.Application.Common.Models;
using OrderingKioskSystem.Application.DashboardCategory.GetPopularCategories;
using OrderingKioskSystem.Application.DashboardCategory;
using OrderingKioskSystem.Application.Overview;
using System.Net.Mime;

namespace OrderingKioskSystemManagement.Api.Controller
{
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly ISender _mediator;

        public DashBoardController(ISender mediator)
        {
            _mediator = mediator;
        }

        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("overview")]
        public async Task<ActionResult<List<OverviewDTO>>> GetOverview()
        {
            var overviewData = await _mediator.Send(new GetOverviewQuery());
            return Ok(new JsonResponse<List<OverviewDTO>>(overviewData));
        }





        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("popular")]
        public async Task<ActionResult<List<PopularCategoryDTO>>> GetPopularCategories(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetPopularCategoriesQuery(), cancellationToken);
            return Ok(new JsonResponse<List<PopularCategoryDTO>>(result));
        }

    }
}