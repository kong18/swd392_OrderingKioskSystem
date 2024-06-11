using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.QRCode
{
    public class CreateQRCommand : IRequest<string>
    {
        public string BusinessID { get; set; }
        public int Amount { get; set; }
    }
}
