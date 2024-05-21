using AutoMapper;
using OrderingKioskSystem.Application.Common.Mappings;
using OrderingKioskSystem.Application.Product;
using OrderingKioskSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Category
{
    public class CategoryDTO : IMapFrom<CategoryEntity>
    {
        public static CategoryDTO Create(int id, string name, string url)
        {
            return new CategoryDTO
            {
                ID = id,
                Name = name,
                Url = url
            };
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CategoryEntity, CategoryDTO>();
        }
    }
}
