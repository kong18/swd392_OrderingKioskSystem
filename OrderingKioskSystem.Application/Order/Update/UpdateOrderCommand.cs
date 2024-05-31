using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Order.Update
{
    public class UpdateOrderCommand : IRequest<string>, ICommand
    {
        public UpdateOrderCommand(string? status, string? shipperID)
        {
            Status = status;
            ShipperID = shipperID;
        }
        public string ID { get; set; }
        public string? Status { get; set; }
        public string? ShipperID { get; set; }
    }
}
