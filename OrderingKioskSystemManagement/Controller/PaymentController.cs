using MediatR;
using Microsoft.AspNetCore.Mvc;
using SWD.OrderingKioskSystem.Application.Payment;

namespace SWD.OrderingKioskSystemManagement.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentCommand command)
        {
            var qrCodeUrl = await _mediator.Send(command);
            return Ok(new { qrCodeUrl });
        }
    }
}
