using AutoMapper;
using OrderingKioskSystem.Application.Product;
using OrderingKioskSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Category
{
    public static class CategoryDTOMappingExtension
    {
        public static CategoryDTO MapToCategoryDTO(this CategoryEntity projectFrom, IMapper mapper)
          => mapper.Map<CategoryDTO>(projectFrom);

        public static List<CategoryDTO> MapToCategoryDTOList(this IEnumerable<CategoryEntity> projectFrom, IMapper mapper)
          => projectFrom.Select(x => x.MapToCategoryDTO(mapper)).ToList();
    }
}
