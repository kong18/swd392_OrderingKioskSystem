using AutoMapper;
using OrderingKioskSystem.Application.Common.Mappings;
using OrderingKioskSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.OrderDetail
{
    public class OrderDetailDTO : IMapFrom<OrderDetailEntity>
    {
        public string ID { get; set; }
        public string ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderDetailEntity, OrderDetailDTO>();
        }
    }
}
