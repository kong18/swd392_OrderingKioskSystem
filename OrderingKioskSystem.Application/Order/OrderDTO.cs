using AutoMapper;
using OrderingKioskSystem.Application.Common.Mappings;
using OrderingKioskSystem.Application.Kiosk;
using OrderingKioskSystem.Application.OrderDetail;
using OrderingKioskSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Order
{
    public class OrderDTO : IMapFrom<OrderEntity>
    {

        public void Mapping(Profile profile)
        {
        }
    }
}
