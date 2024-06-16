using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderingKioskSystem.Application.Common.Mappings;
using OrderingKioskSystem.Domain.Entities;
using System.Collections.Generic;

namespace OrderingKioskSystem.Application.Menu
{
    public class MenuDTO : IMapFrom<MenuEntity>
    {
        [BindProperty(Name = "id")]
        public string ID { get; set; }

        [BindProperty(Name = "title")]
        public string Title { get; set; }

        [BindProperty(Name = "name")]
        public string Name { get; set; }

        [BindProperty(Name = "status")]
        public bool Status { get; set; }

        [BindProperty(Name = "type")]
        public string Type { get; set; }

        [BindProperty(Name = "business-id")]
        public string BusinessID { get; set; }

        [BindProperty(Name = "business-name")]
        public string BusinessName { get; set; }

        [BindProperty(Name = "products")]
        public List<ResponseItem> Products { get; set; } = new List<ResponseItem>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MenuEntity, MenuDTO>()
                .ForMember(dto => dto.BusinessName, opt => opt.MapFrom(entity => entity.Business.Name))
                .ForMember(dto => dto.Products, opt => opt.MapFrom(entity => entity.ProductMenus));

            profile.CreateMap<ProductMenuEntity, ResponseItem>()
                .ForMember(dto => dto.Name, opt => opt.MapFrom(entity => entity.Product.Name))
                .ForMember(dto => dto.ProductID, opt => opt.MapFrom(entity => entity.ProductID))
                .ForMember(dto => dto.Url, opt => opt.MapFrom(entity => entity.Product.Url))
                .ForMember(dto => dto.Price, opt => opt.MapFrom(entity => entity.Price));
        }
    }

    public class ResponseItem
    {
        [BindProperty(Name = "product-id")]
        public string ProductID { get; set; }

        [BindProperty(Name = "name")]
        public string Name { get; set; }

        [BindProperty(Name = "url")]
        public string Url { get; set; }

        [BindProperty(Name = "price")]
        public decimal Price { get; set; }
    }
}
