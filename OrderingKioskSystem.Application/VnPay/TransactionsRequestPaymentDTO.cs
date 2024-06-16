using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.VNPay
{
    public class TransactionsRequestPaymentDTO
    {
        public int Amount { get; set; }
        public string KioskID { get; set; }
        public string? Note { get; set; }
        public decimal Total { get; set; }
        public List<RequestItem> Products { get; set; }
    }

    public class RequestItem
    {
        public string ProductID { get; set; }
        public int Quantity { get; set; }
        public string? Size { get; set; }
    }

}
