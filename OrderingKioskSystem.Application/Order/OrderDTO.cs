using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderingKioskSystem.Application.Common.Mappings;
using OrderingKioskSystem.Domain.Entities;
using System;
using System.Collections.Generic;

namespace OrderingKioskSystem.Application.Order
{
    public class OrderDTO : IMapFrom<OrderEntity>
    {
        [BindProperty(Name = "id")]
        public string ID { get; set; }

        [BindProperty(Name = "kiosk-id")]
        public string KioskID { get; set; }

        [BindProperty(Name = "location")]
        public string Location { get; set; }

        [BindProperty(Name = "total")]
        public decimal Total { get; set; }

        [BindProperty(Name = "note")]
        public string Note { get; set; }

        [BindProperty(Name = "status")]
        public string Status { get; set; }

        [BindProperty(Name = "ngay-tao")]
        public DateTime NgayTao { get; set; }

        [BindProperty(Name = "shipper-name")]
        public string? ShipperName { get; set; }

        [BindProperty(Name = "products")]
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
        [BindProperty(Name = "product-id")]
        public string ProductID { get; set; }

        [BindProperty(Name = "name")]
        public string Name { get; set; }

        [BindProperty(Name = "unit-price")]
        public decimal UnitPrice { get; set; }

        [BindProperty(Name = "quantity")]
        public int Quantity { get; set; }

        [BindProperty(Name = "price")]
        public decimal Price { get; set; }

        [BindProperty(Name = "size")]
        public string? Size { get; set; }
    }
}
