using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OrderingKioskSystem.Application.Order;
using System;
using System.Globalization;

namespace SWD.OrderingKioskSystem.Application.VNPay
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _configuration;

        public VnPayService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreatePaymentUrl(OrderDTO model, HttpContext context)
        {
            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]);
            var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            var pay = new VnPayLibrary();
            var urlCallBack = _configuration["Vnpay:ReturnUrl"];

            pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
            pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
            pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
            pay.AddRequestData("vnp_Amount", (model.Total * 100).ToString());
            pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture));
            pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
            pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
            pay.AddRequestData("vnp_OrderInfo", model.Note);
            pay.AddRequestData("vnp_OrderType", "billpayment");
            pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
            pay.AddRequestData("vnp_TxnRef", model.ID);

            // Optional: Add vnp_ExpireDate if needed
            var expireDate = timeNow.AddMinutes(15); // Example: 15 minutes expiration
            pay.AddRequestData("vnp_ExpireDate", expireDate.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture));

            var paymentUrl = pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);

            return paymentUrl;
        }

        public TransactionResponsePaymentDTO PaymentExecute(IQueryCollection collections)
        {
            var pay = new VnPayLibrary();
            var response = pay.GetFullResponseData(collections, _configuration["Vnpay:HashSecret"]);

            return response;
        }
    }
}
