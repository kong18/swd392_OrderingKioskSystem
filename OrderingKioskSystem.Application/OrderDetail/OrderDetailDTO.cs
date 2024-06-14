using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderingKioskSystem.Application.Common.Mappings;
using OrderingKioskSystem.Domain.Entities;

namespace OrderingKioskSystem.Application.OrderDetail
{
    public class OrderDetailDTO : IMapFrom<OrderDetailEntity>
    {
        [BindProperty(Name = "id")]
        public string ID { get; set; }

        [BindProperty(Name = "product-id")]
        public string ProductID { get; set; }

        [BindProperty(Name = "quantity")]
        public int Quantity { get; set; }

        [BindProperty(Name = "unit-price")]
        public decimal UnitPrice { get; set; }

        [BindProperty(Name = "price")]
        public decimal Price { get; set; }

        [BindProperty(Name = "size")]
        public string Size { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderDetailEntity, OrderDetailDTO>();
        }
    }
}
