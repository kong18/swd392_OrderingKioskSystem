using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Shipper.DeleteShipper
{
    public class DeleteShipperCommand : IRequest<string>
    {
        public string Id;
        public DeleteShipperCommand(string id)
        {
            Id = id;
        }
    }
}
