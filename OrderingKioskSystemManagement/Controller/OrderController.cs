using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderingKioskSystem.Application.Order.Create;
using OrderingKioskSystem.Application.Product;
using System.Net.Mime;
using OrderingKioskSystem.Application.Order.GetById;
using OrderingKioskSystem.Application.Order;
using OrderingKioskSystem.Application.Order.Update;
using OrderingKioskSystem.Application.Order.Delete;
using OrderingKioskSystem.Application.Order.Filter;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Text;
using SWD.OrderingKioskSystem.Application.QRCode;
using System.Threading;
using OrderingKioskSystemManagement.Api.Controller;

namespace OrderingKioskSystemManagement.Api.Controllers
{
    [ApiController]
    [Route("api/v1/orders")]
    public class OrderController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly OrderService _orderService;
        private readonly RegisterWebhook _registerWebhook;
        private readonly ILogger<OrderController> _logger;

        public OrderController(ISender mediator, OrderService orderService, RegisterWebhook registerWebhook, ILogger<OrderController> logger)
        {
            _mediator = mediator;
            _orderService = orderService;
            _registerWebhook = registerWebhook;
            _logger = logger;
        }

        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateOrder(
           [FromBody] CreateOrderCommand command,
           CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<OrderDTO>(result));
        }

        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OrderDTO>> GetOrderByID(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetOrderByIdQuery(id), cancellationToken);
            return Ok(new JsonResponse<OrderDTO>(result));
        }


        [HttpPut("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateOrder(
            [FromRoute] string id,
            [FromBody] UpdateOrderCommand command,
            CancellationToken cancellationToken = default)
        {
            command.ID = id; // Ensure the command has the id
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteOrder(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteOrderCommand(id), cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        

       

        [HttpPost("webhook")]
        public async Task<IActionResult> PaymentWebhook([FromBody] VietQRPaymentStatusUpdate update)
        {
            try
            {
                if (update.Status == "success")
                {
                    await _orderService.NotifyNewOrder(update.OrderId);
                    return Ok(new { message = "Payment success notification sent to frontend." });
                }

                return BadRequest(new { message = "Invalid status." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the webhook.");
                return StatusCode(500, new { message = "An internal server error occurred.", detail = ex.Message });
            }
        }

        [HttpPost("register-webhook")]
        public async Task<IActionResult> RegisterWebhook([FromBody] RegisterWebhookRequest request)
        {
            try
            {
                var result = await _registerWebhook.RegiterWebHook(request);

                if (result == "Success")
                {
                    return Ok(new { message = "Webhook registered successfully." });
                }
                else
                {
                    return StatusCode(500, new { message = "Failed to register webhook." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering webhook.");
                return StatusCode(500, new { message = "An internal server error occurred.", detail = ex.Message });
            }
        }
    }

    public class VietQRPaymentStatusUpdate
    {
        public string Status { get; set; }
        public string OrderId { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string TransactionId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
