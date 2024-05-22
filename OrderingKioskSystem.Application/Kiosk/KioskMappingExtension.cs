using AutoMapper;
using OrderingKioskSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Kiosk
{
    public static class KioskMappingExtension
    {
        public static KioskDTO MapToKioskDTO(this KioskEntity projectfrom, IMapper mapper)
        {
            return mapper.Map<KioskDTO>(projectfrom);

        }
        public static List<KioskDTO> MapToKioskDTOList(this IEnumerable<KioskEntity> projectFrom, IMapper mapper)
           => projectFrom.Select(x => x.MapToKioskDTO(mapper)).ToList();
    }
}
