using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Category.Delete;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Category.GetById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDTO>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var categoryExist = await _categoryRepository.FindAsync(x => x.ID == request.ID, cancellationToken);

            if (categoryExist is null || categoryExist.NgayXoa.HasValue)
            {
                throw new NotFoundException("Category is not found or deleted");
            }

            return categoryExist.MapToCategoryDTO(_mapper);
        }
    }
}
