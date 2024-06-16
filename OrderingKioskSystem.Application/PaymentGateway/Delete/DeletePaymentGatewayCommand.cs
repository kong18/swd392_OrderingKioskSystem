using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.PaymentGateway.Delete
{
    public class DeletePaymentGatewayCommand : IRequest<string>
    {
        public DeletePaymentGatewayCommand(int id)
        {
            ID = id;
        }

        public int ID { get; set; }
    }
}
