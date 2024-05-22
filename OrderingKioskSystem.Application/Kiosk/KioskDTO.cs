using AutoMapper;
using OrderingKioskSystem.Application.Category;
using OrderingKioskSystem.Application.Common.Mappings;
using OrderingKioskSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Kiosk
{
    public class KioskDTO : IMapFrom<KioskEntity>
    {
        public required string location { get; set;}
        public void Mapping(Profile profile)
        {
            profile.CreateMap<KioskEntity, KioskDTO>();
                
        }
    }
}
