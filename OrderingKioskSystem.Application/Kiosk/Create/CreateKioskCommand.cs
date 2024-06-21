using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Kiosk.Create
{
    public class CreateKioskCommand : IRequest<string>, ICommand
    {
        public CreateKioskCommand(string location, string code, int pin)
        {
            Location = location;
            Code = code;
            PIN = pin;
        }

        public string Location { get; set; }
        public string Code { get; set; }
        public int PIN { get; set; }


    }
}
