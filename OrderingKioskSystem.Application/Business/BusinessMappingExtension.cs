using AutoMapper;
using OrderingKioskSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Business
{
    public static class BusinessMappingExtension
    {
        public static BusinessDTO MapToBusinessDTO(this BusinessEntity projectfrom, IMapper mapper)
        {
            return mapper.Map<BusinessDTO>(projectfrom);

        }
        public static List<BusinessDTO> MapToBusinessDTOList(this IEnumerable<BusinessEntity> projectFrom, IMapper mapper)
           => projectFrom.Select(x => x.MapToBusinessDTO(mapper)).ToList();
    }
}
