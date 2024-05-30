using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Shipper.UpdateShipper
{
    public class UpdateShipperCommand : IRequest<string>, ICommand
    {
        public string Id { get; set; }
        public string ShipperName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
