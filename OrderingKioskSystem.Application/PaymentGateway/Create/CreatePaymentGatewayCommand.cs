using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.PaymentGateway.Create
{
    public class CreatePaymentGatewayCommand : IRequest<string>
    {
        public CreatePaymentGatewayCommand(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
