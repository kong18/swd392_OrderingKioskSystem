using MediatR;
using System;

namespace SWD.OrderingKioskSystem.Application.Payment
{
    public class CreatePaymentCommand : IRequest<string>
    {
        public string BusinessID { get; set; } // Thêm BusinessID vào request để lấy thông tin business
        public int AcqId { get; set; }
        public int Amount { get; set; }
        public string AddInfo { get; set; }
        public string OrderID { get; set; }
        public int PaymentGatewayID { get; set; }
    }
}
