using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SWD.OrderingKioskSystem.Application.VNPay;
using OrderingKioskSystem.Infrastructure.Persistence;
using System.Threading.Tasks;
using OrderingKioskSystem.Application.Order.Create;
using OrderingKioskSystem.Application.Order;
using OrderingKioskSystemManagement.Api.Controller;
using System.Net.Mime;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingKioskSystem.Application.Order.GetById;
using OrderingKioskSystem.Domain.Repositories;

namespace SWD.OrderingKioskSystemManagement.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class VNPayController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public VNPayController(IVnPayService vnPayService, IHttpContextAccessor httpContextAccessor, IOrderRepository orderRepository, IMapper mapper, ISender mediator)
        {
            _vnPayService = vnPayService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _mediator = mediator;
            _orderRepository = orderRepository;
        }

        [HttpPost]
        public async Task<ActionResult> CreatePaymentUrl(
            [FromBody] CreateOrderCommand command,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);

            var url = _vnPayService.CreatePaymentUrl(result, _httpContextAccessor.HttpContext);
            return Ok(new { url });
        }

        [HttpGet("response")]
        public async Task<IActionResult> PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            if (response.Success && response.VnPayResponseCode == "00")
            {
                // Process the payment success logic here (e.g., update database)
                var orderId = Request.Query["vnp_TxnRef"].ToString();
                var order = await _orderRepository.FindAsync(o => o.ID == orderId);

                if (order != null)
                {
                    order.Status = "Paid";
                    _orderRepository.Update(order);
                    await _orderRepository.UnitOfWork.SaveChangesAsync();
                }

                return Ok(new { message = "Payment successful!", response });
            }

            return BadRequest(response);
        }
    }
}
