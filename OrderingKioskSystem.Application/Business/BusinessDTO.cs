using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderingKioskSystem.Application.Common.Mappings;
using OrderingKioskSystem.Application.Product;
using OrderingKioskSystem.Domain.Entities;
using System.Collections.Generic;

namespace OrderingKioskSystem.Application.Business
{
    public class BusinessDTO : IMapFrom<BusinessEntity>
    {
        [BindProperty(Name = "id")]
        public string ID { get; set; }

        [BindProperty(Name = "url")]
        public string Url { get; set; }

        [BindProperty(Name = "bin-id")]
        public string BinId { get; set; }

        [BindProperty(Name = "name")]
        public string Name { get; set; }

        [BindProperty(Name = "bank-account-number")]
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }

        [BindProperty(Name = "bank-account-name")]
        public string BankAccountName { get; set; }

        [BindProperty(Name = "bank-name")]
        public string BankName { get; set; }

        [BindProperty(Name = "products")]
        public List<ProductDTO> Products { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BusinessEntity, BusinessDTO>();
        }
    }
}
