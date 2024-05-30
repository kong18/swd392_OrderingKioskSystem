using AutoMapper;
using OrderingKioskSystem.Application.Common.Mappings;
using OrderingKioskSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Shipper
{
    public class ShipperDTO : IMapFrom<ShipperEntity>
    {
        public string ID {  get; set; }
        public string Name { get; set; }
        public string Address {  get; set; }
        public string Phone {  get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ShipperEntity,ShipperDTO>();

        }


    }
}
