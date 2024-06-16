using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderingKioskSystem.Application.Common.Mappings;
using OrderingKioskSystem.Domain.Entities;

namespace OrderingKioskSystem.Application.Shipper
{
    public class ShipperDTO : IMapFrom<ShipperEntity>
    {
        [BindProperty(Name = "id")]
        public string ID { get; set; }

        [BindProperty(Name = "name")]
        public string Name { get; set; }

        [BindProperty(Name = "address")]
        public string Address { get; set; }

        [BindProperty(Name = "phone")]
        public string Phone { get; set; }
        public string Email { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ShipperEntity, ShipperDTO>();
        }
    }
}
