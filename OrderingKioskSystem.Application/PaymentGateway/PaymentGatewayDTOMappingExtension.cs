using AutoMapper;
using OrderingKioskSystem.Application.Category;
using OrderingKioskSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.PaymentGateway
{
    public static class PaymentGatewayDTOMappingExtension
    {
        public static PaymentGatewayDTO MapToPaymentGatewayDTO(this PaymentGatewayEntity projectFrom, IMapper mapper)
          => mapper.Map<PaymentGatewayDTO>(projectFrom);

        public static List<PaymentGatewayDTO> MapToPaymentGatewayDTOList(this IEnumerable<PaymentGatewayEntity> projectFrom, IMapper mapper)
          => projectFrom.Select(x => x.MapToPaymentGatewayDTO(mapper)).ToList();
    }
}
