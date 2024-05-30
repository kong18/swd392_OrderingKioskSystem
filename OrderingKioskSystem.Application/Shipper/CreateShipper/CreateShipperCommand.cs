using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Shipper.CreateShipper
{
    public class CreateShipperCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string ShipperName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
