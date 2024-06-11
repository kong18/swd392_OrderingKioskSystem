using MediatR;
using OrderingKioskSystem.Application.Business;
using OrderingKioskSystem.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Shipper.GetAllShipper
{
    public class GetShipperQuery : IRequest<List<ShipperDTO>>, IQuery
    {
    }
}
