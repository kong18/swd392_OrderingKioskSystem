﻿using Microsoft.AspNetCore.Mvc;
using SWD.OrderingKioskSystem.Application.VNPay;
using System.Threading.Tasks;
using OrderingKioskSystem.Application.Order.Create;
using System.Net.Mime;
using MediatR;
using OrderingKioskSystem.Domain.Repositories;
using SWD.OrderingKioskSystem.Application.Payment;
using Microsoft.AspNetCore.Authorization;

namespace SWD.OrderingKioskSystemManagement.Api.Controller
{
    [Route("api/v1/vnpay")]
    [ApiController]
    public class VNPayController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrderRepository _orderRepository;
        private readonly ISender _mediator;
        private readonly IPaymentService _paymentService;

        public VNPayController(IVnPayService vnPayService, IHttpContextAccessor httpContextAccessor, IOrderRepository orderRepository, ISender mediator, IPaymentService paymentService)
        {
            _vnPayService = vnPayService;
            _httpContextAccessor = httpContextAccessor;
            _mediator = mediator;
            _orderRepository = orderRepository;
            _paymentService = paymentService;
        }

        [HttpPost]
        [Authorize(Roles = "Kiosk")]
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
                var order = await _orderRepository.FindAsync(o => o.ID == orderId && !o.NgayXoa.HasValue);

                if (order != null)
                {
                    order.Status = "Paid";
                    _orderRepository.Update(order);
                    await _orderRepository.UnitOfWork.SaveChangesAsync();
                } else
                {
                    return BadRequest(new { message = "Payment failed! Order is not found or expired." });
                }

                await _paymentService.SavePaymentAsync(response);

                response.Amount /= 100;

                return Ok(new { message = "Payment successful!", response });
            }

            return BadRequest(response);
        }
    }
}
