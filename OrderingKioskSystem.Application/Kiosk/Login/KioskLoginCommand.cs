using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.Kiosk.Login
{
    public class KioskLoginCommand : IRequest<string>
    {
        public string Code { get; set; }
        public int PIN { get; set; }

        public KioskLoginCommand(string code, int pin)
        {
            Code = code;
            PIN = pin;
        }
    }
}
