using Microsoft.AspNetCore.Http;
using OrderingKioskSystem.Application.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.VNPay
{
    public interface IVnPayService
    {
        public string CreatePaymentUrl(OrderDTO model, HttpContext context);
        public TransactionResponsePaymentDTO PaymentExecute(IQueryCollection collections);
    }
}

