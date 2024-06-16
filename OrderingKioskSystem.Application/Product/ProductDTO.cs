using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderingKioskSystem.Application.Common.Mappings;
using OrderingKioskSystem.Domain.Entities;

namespace OrderingKioskSystem.Application.Product
{
    public class ProductDTO : IMapFrom<ProductEntity>
    {
        public ProductDTO() { }

        public static ProductDTO Create(string id, string name, string code, string url, string description, decimal price, bool status, int categoryID, string businessID)
        {
            return new ProductDTO
            {
                ID = id,
                Name = name,
                Code = code,
                Url = url,
                Description = description,
                Price = price,
                Status = status,
                CategoryID = categoryID,
                BusinessID = businessID
            };
        }

        [BindProperty(Name = "id")]
        public string ID { get; set; }

        [BindProperty(Name = "name")]
        public string Name { get; set; }

        [BindProperty(Name = "code")]
        public string Code { get; set; }

        [BindProperty(Name = "url")]
        public string Url { get; set; }

        [BindProperty(Name = "description")]
        public string Description { get; set; }

        [BindProperty(Name = "price")]
        public decimal Price { get; set; }

        [BindProperty(Name = "status")]
        public bool Status { get; set; }

        [BindProperty(Name = "category-id")]
        public int CategoryID { get; set; }

        [BindProperty(Name = "category-name")]
        public string CategoryName { get; set; }

        [BindProperty(Name = "business-id")]
        public string BusinessID { get; set; }

        [BindProperty(Name = "business-name")]
        public string BusinessName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductEntity, ProductDTO>()
                .ForMember(dto => dto.CategoryName, opt => opt.MapFrom(entity => entity.Category.Name))
                .ForMember(dto => dto.BusinessName, opt => opt.MapFrom(entity => entity.Business.Name));
        }
    }
}
