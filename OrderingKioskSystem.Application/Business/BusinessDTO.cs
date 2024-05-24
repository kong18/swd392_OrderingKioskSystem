using AutoMapper;
using OrderingKioskSystem.Application.Common.Mappings;
using OrderingKioskSystem.Application.Product;
using OrderingKioskSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Business
{
    public class BusinessDTO : IMapFrom<BusinessEntity>
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankAccountName { get; set; }
        public string BankName { get; set; }
        public List<ProductDTO> Products { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BusinessEntity,BusinessDTO>();
        }
    }
}
