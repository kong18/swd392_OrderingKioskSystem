using AutoMapper;
using OrderingKioskSystem.Application.Common.Mappings;
using OrderingKioskSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.User
{
    public class UserDTO : IMapFrom<UserEntity>
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
      

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserEntity, UserDTO>()
                     .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Business != null ? src.Business.Name : string.Empty))
                     .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Shipper != null ? src.Shipper.Name : string.Empty))
                     .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Manager != null ? src.Manager.Name : string.Empty));
        }
    }
}
