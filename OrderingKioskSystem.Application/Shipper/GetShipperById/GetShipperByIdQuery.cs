using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Shipper.GetShipperById
{
    public class GetShipperByIdQuery : IRequest<ShipperDTO>, IQuery
    {
       public string Id { get; set; }
        public GetShipperByIdQuery(string id)
        {
            Id = id;
        }   


    }
}
