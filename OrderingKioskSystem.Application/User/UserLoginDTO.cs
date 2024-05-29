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
    public class UserLoginDTO : IMapFrom<UserEntity>
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public string EntityId { get; set; } // ID of Business, Manager, or Shipper entity

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserEntity, UserLoginDTO>();
        }
    }
}
