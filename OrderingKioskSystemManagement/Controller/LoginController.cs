using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Application.User.Authenticate;
using OrderingKioskSystemManagement.Api.Controller;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using SWD.OrderingKioskSystem.Application.User.Authenticate;
using SWD.OrderingKioskSystem.Application.Kiosk.Login;

namespace OrderingKioskSystemManagement.Api.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
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
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            var token = _jwtService.CreateToken(result.EntityId, result.Role, result.Email);
            return Ok(new JsonResponse<object>(new
            {
                token = token,
                role = result.Role
            }));
        }

        [HttpPost("login-with-google")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginGoogleQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            var token = _jwtService.CreateToken(result.EntityId, result.Role, result.Email);
            return Ok(new JsonResponse<object>(new { 
                token = token,
                role = result.Role
            }));
        }

        [HttpPost("login-kiosk")]
        public async Task<IActionResult> Login([FromBody] KioskLoginCommand command)
        {
            var token = await _mediator.Send(command);
            return Ok(new { Token = token });
        }




        //[HttpGet]
        //[Route("login-with-google/business")]
        //[AllowAnonymous]
        //public IActionResult LoginWithGoogleBusiness()
        //{
        //    var properties = new AuthenticationProperties
        //    {
        //        RedirectUri = Url.Action("GoogleLoginRedirect", new { role = "Business" }),
        //        Items = { { "scheme", GoogleDefaults.AuthenticationScheme } }
        //    };
        //    return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        //}

        //[HttpGet]
        //[Route("login-with-google/manager")]
        //[AllowAnonymous]
        //public IActionResult LoginWithGoogleManager()
        //{
        //    var properties = new AuthenticationProperties
        //    {
        //        RedirectUri = Url.Action("GoogleLoginRedirect", new { role = "Manager" }),
        //        Items = { { "scheme", GoogleDefaults.AuthenticationScheme } }
        //    };
        //    return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        //}

        //[HttpGet]
        //[Route("redirect-google")]
        //[AllowAnonymous]
        //public async Task<IActionResult> GoogleLoginRedirect([FromQuery] string role)
        //{
        //    var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        //    if (!result.Succeeded)
        //    {
        //        Log.Error("Google authentication failed.");
        //        return BadRequest(new { Error = "Failed to authenticate with Google" });
        //    }

        //    var email = result.Principal.FindFirst(ClaimTypes.Email)?.Value;

        //    if (string.IsNullOrEmpty(email))
        //    {
        //        Log.Error("Email claim not found in Google authentication response.");
        //        return BadRequest(new { Error = "Email claim not found" });
        //    }

        //    try
        //    {
        //        Log.Information($"Authenticated with Google. Email: {email}, Role: {role}");
        //        var command = new GoogleLoginCommand(email, role);
        //        var jwtToken = await _mediator.Send(command);
        //        Log.Information($"JWT token created for email: {email}");
        //        return Ok(new { accessToken = jwtToken });
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex, "An error occurred during Google login.");
        //        return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "An error occurred during authentication", Details = ex.Message });
        //    }
        //}




    }
}
