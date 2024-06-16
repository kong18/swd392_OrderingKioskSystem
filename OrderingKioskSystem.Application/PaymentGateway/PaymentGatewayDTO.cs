using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderingKioskSystem.Application.Category;
using OrderingKioskSystem.Application.Common.Mappings;
using OrderingKioskSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.PaymentGateway
{
    public class PaymentGatewayDTO : IMapFrom<PaymentGatewayEntity>
    {
        public static PaymentGatewayDTO Create(int id, string name)
        {
            return new PaymentGatewayDTO
            {
                ID = id,
                Name = name
            };
        }
        public int ID { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PaymentGatewayEntity, PaymentGatewayDTO>();
        }
    }
}
