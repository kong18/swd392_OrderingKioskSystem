using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderingKioskSystem.Application.Order;
using SWD.OrderingKioskSystem.Application.Payment;
using SWD.OrderingKioskSystem.Application.VnPay;

namespace SWD.OrderingKioskSystemManagement.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        public PaymentController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpGet("create-payment")]
        public IActionResult CreatePayment()
        {
            string vnp_Returnurl = _configuration["VNPay:ReturnUrl"];
            string vnp_Url = _configuration["VNPay:Url"];
            string vnp_TmnCode = _configuration["VNPay:TmnCode"];
            string vnp_HashSecret = _configuration["VNPay:HashSecret"];

            var vnPayLibrary = new VnPayLibrary(_configuration);
            vnPayLibrary.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnPayLibrary.AddRequestData("vnp_Amount", (1000000 * 100).ToString()); // Amount in VND
            vnPayLibrary.AddRequestData("vnp_Command", "pay");
            vnPayLibrary.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnPayLibrary.AddRequestData("vnp_CurrCode", "VND");
            vnPayLibrary.AddRequestData("vnp_IpAddr", Util.GetIpAddress(HttpContext));
            vnPayLibrary.AddRequestData("vnp_Locale", "vn");
            vnPayLibrary.AddRequestData("vnp_OrderInfo", "Payment for Order ID: " + DateTime.Now.Ticks);
            vnPayLibrary.AddRequestData("vnp_OrderType", "other");
            vnPayLibrary.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnPayLibrary.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString());

            string paymentUrl = vnPayLibrary.CreatePaymentUrl(vnp_Url, vnp_HashSecret, vnp_Returnurl);
            return Ok(new { paymentUrl });
        }

        [HttpGet("payment-return")]
        public IActionResult PaymentReturn()
        {
            var vnPayLibrary = new VnPayLibrary();
            foreach (var (key, value) in Request.Query)
            {
                vnPayLibrary.AddResponseData(key, value);
            }

            string vnp_HashSecret = _configuration["VNPay:HashSecret"];
            string vnp_SecureHash = Request.Query["vnp_SecureHash"];
            bool checkSignature = vnPayLibrary.ValidateSignature(vnp_SecureHash, vnp_HashSecret);

            if (checkSignature)
            {
                // Process your payment result here
                string vnp_TransactionStatus = vnPayLibrary.GetResponseData("vnp_TransactionStatus");
                if (vnp_TransactionStatus == "00")
                {
                    // Payment success
                    return Ok(new { message = "Payment successful!" });
                }
                else
                {
                    // Payment failed
                    return BadRequest(new { message = "Payment failed!" });
                }
            }
            else
            {
                return BadRequest(new { message = "Invalid signature!" });
            }
        }
    }
}
