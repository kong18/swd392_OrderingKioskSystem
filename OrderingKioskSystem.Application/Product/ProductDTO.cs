using AutoMapper;
using OrderingKioskSystem.Application.Common.Mappings;
using OrderingKioskSystem.Domain.Entities;

namespace OrderingKioskSystem.Application.Product
{
    public class ProductDTO : IMapFrom<ProductEntity>
    {
        public ProductDTO() { }

        public static ProductDTO Create(string id, string name, string code, string url, string description, decimal price, bool isAvailable, bool status, int categoryID, string businessID)
        {
            return new ProductDTO
            {
                ID = id,
                Name = name,
                Code = code,
                Url = url,
                Description = description,
                Price = price,
                IsAvailable = isAvailable,
                Status = status,
                CategoryID = categoryID,
                BusinessID = businessID
            };
        }

        public string ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public bool Status { get; set; }
        public int CategoryID { get; set; }
        public string BusinessID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductEntity, ProductDTO>();
            profile.CreateMap<ProductDTO, ProductEntity>();
        }
    }
}
