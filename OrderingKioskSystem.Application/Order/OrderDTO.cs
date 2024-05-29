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
        public string ID { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public string KioskID { get; set; }
        public string Location { get; set; } 

        public List<OrderDetailDTO> OrderDetails { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderEntity, OrderDTO>().ForMember(dest => dest.KioskID, opt => opt.MapFrom(src => src.KioskID))
                   .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Kiosk.Location)).ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails));
        }
    }
}
