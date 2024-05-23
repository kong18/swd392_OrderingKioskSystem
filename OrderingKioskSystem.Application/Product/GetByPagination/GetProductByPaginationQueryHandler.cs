using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Common.Pagination;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OrderingKioskSystem.Application.Product.GetByPagination
{
    public class GetProductByPaginationQueryHandler : IRequestHandler<GetProductByPaginationQuery, PagedResult<ProductDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetProductByPaginationQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ProductDTO>> Handle(GetProductByPaginationQuery query, CancellationToken cancellationToken)
        {
            var list = await _productRepository.FindAllAsync(x => x.NgayXoa == null, query.PageNumber, query.PageSize, cancellationToken);
            return PagedResult<ProductDTO>.Create
            (
                totalCount: list.TotalCount,
                pageCount: list.PageCount,
                pageSize: list.PageSize,
                pageNumber: list.PageNo,
                data: list.MapToProductDTOList(_mapper)
                );
        }
    }
}
