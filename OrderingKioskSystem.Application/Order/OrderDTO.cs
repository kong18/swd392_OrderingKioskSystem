using AutoMapper;
using OrderingKioskSystem.Application.Category;
using OrderingKioskSystem.Application.Common.Mappings;
using OrderingKioskSystem.Application.Kiosk;
using OrderingKioskSystem.Application.Order.Create;
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
        public string KioskID { get; set; }
        public string Location { get; set; }
        public decimal Total {  get; set; }
        public string Note { get; set; }
        public string? ShipperName { get; set; }
        public List<ResponseItem> Products { get; set; } = new List<ResponseItem>();
        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderEntity, OrderDTO>()
                .ForMember(dto => dto.ShipperName, opt => opt.MapFrom(entity => entity.Shipper.Name))
                .ForMember(dto => dto.Products, opt => opt.MapFrom(entity => entity.OrderDetails))
                .ForMember(dto => dto.Location, opt => opt.MapFrom(entity => entity.Kiosk.Location));

            profile.CreateMap<OrderDetailEntity, ResponseItem>()
                .ForMember(dto => dto.Name, opt => opt.MapFrom(entity => entity.Product.Name))
                .ForMember(dto => dto.UnitPrice, opt => opt.MapFrom(entity => entity.UnitPrice))
                .ForMember(dto => dto.Quantity, opt => opt.MapFrom(entity => entity.Quantity))
                .ForMember(dto => dto.Price, opt => opt.MapFrom(entity => entity.Price))
                .ForMember(dto => dto.ProductID, opt => opt.MapFrom(entity => entity.ProductID))
                .ForMember(dto => dto.Size, opt => opt.MapFrom(entity => entity.Size));
        }
    }

    public class ResponseItem
    {
        public string ProductID { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? Size { get; set; }
    }
}
