using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Application.User.Authenticate;
using OrderingKioskSystemManagement.Api.Controller;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingKioskSystemManagement.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IJwtService _jwtService;

        public UserController(IMediator mediator, IJwtService jwtService)
        {
            _mediator = mediator;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            var token = _jwtService.CreateToken(result.EntityId, result.Role);
            return Ok(new JsonResponse<string>(token));
        }
    }
}
