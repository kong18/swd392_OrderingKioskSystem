using AutoMapper;
using OrderingKioskSystem.Application.Business;
using OrderingKioskSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Shipper
{
    public static class ShipperMappingExtension
    {
        public static ShipperDTO MapToShipperDTO(this ShipperEntity projectfrom, IMapper mapper)
        {
            return mapper.Map<ShipperDTO>(projectfrom);

        }
        public static List<ShipperDTO> MapToShipperDTOist(this IEnumerable<ShipperEntity> projectFrom, IMapper mapper)
           => projectFrom.Select(x => x.MapToShipperDTO(mapper)).ToList();
    }
}
