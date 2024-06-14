using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SWD.OrderingKioskSystem.Application.VNPay;
using OrderingKioskSystem.Infrastructure.Persistence;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystemManagement.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class VNPayController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public VNPayController(IVnPayService vnPayService, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context, IMapper mapper)
        {
            _vnPayService = vnPayService;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreatePaymentUrl(TransactionsRequestPaymentDTO model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, _httpContextAccessor.HttpContext);
            return Ok(new { url });
        }

        [HttpGet("response")]
        public async Task<IActionResult> PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            if (response.Success && response.VnPayResponseCode == "00")
            {
                // Process the payment success logic here (e.g., update database)
                await _context.SaveChangesAsync();
                return Ok(new { message = "Payment successful!", response });
            }

            return BadRequest(response);
        }
    }
}
