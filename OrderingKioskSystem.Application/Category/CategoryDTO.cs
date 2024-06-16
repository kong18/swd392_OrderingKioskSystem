using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderingKioskSystem.Application.Common.Mappings;
using OrderingKioskSystem.Application.Product;
using OrderingKioskSystem.Domain.Entities;
using System.Collections.Generic;

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

        [BindProperty(Name = "id")]
        public int ID { get; set; }

        [BindProperty(Name = "name")]
        public string Name { get; set; }

        [BindProperty(Name = "url")]
        public string Url { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CategoryEntity, CategoryDTO>();
        }
    }
}
