using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.VNPay
{
    public interface IVnPayService
    {
        public string CreatePaymentUrl(TransactionsRequestPaymentDTO model, HttpContext context);
        public TransactionResponsePaymentDTO PaymentExecute(IQueryCollection collections);
    }
}

