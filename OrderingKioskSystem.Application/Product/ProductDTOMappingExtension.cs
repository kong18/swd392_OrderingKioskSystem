using AutoMapper;
using OrderingKioskSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Product
{
    public static class ProductDTOMappingExtension
    {
        public static ProductDTO MapToProductDTO(this ProductEntity projectFrom, IMapper mapper)
          => mapper.Map<ProductDTO>(projectFrom);

        public static List<ProductDTO> MapToProductDTOList(this IEnumerable<ProductEntity> projectFrom, IMapper mapper)
          => projectFrom.Select(x => x.MapToProductDTO(mapper)).ToList();
    }
}
