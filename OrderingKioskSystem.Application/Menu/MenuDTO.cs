using AutoMapper;
using OrderingKioskSystem.Application.Business;
using OrderingKioskSystem.Application.Common.Mappings;
using OrderingKioskSystem.Application.Order;
using OrderingKioskSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Menu
{
    public class MenuDTO : IMapFrom<MenuEntity>
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Type { get; set; }
        public string BusinessID {  get; set; }
        public string BusinessName { get; set; }

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
        public string ProductID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public decimal Price { get; set; }
    }
}
