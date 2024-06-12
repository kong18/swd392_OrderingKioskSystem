using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Category.GetById;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Category.GetAll
{
    public class SearchCategoryQueryHandler : IRequestHandler<SearchCategory, List<CategoryDTO>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;


        public SearchCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryDTO>> Handle(SearchCategory request, CancellationToken cancellationToken)
        {
            var query = await _categoryRepository.FindAllAsync(c => c.NgayXoa == null, cancellationToken);

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(b => b.Name.Contains(request.Name)).ToList();
            }

            return query.MapToCategoryDTOList(_mapper); ;
        }
    }
}
