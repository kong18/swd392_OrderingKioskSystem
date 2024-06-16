using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingKioskSystem.Application.Common.Pagination;
using OrderingKioskSystem.Infrastructure.Persistence;
using OrderingKioskSystem.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Product.Filter
{
    public class FilterProductQueryHandler : IRequestHandler<FilterProductQuery, PagedResult<ProductDTO>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FilterProductQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResult<ProductDTO>> Handle(FilterProductQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(request.name))
            {
                query = query.Where(p => p.Name.Contains(request.name));
            }

            if (!string.IsNullOrEmpty(request.code))
            {
                query = query.Where(p => p.Code.Contains(request.code));
            }

            if (request.status.HasValue)
            {
                query = query.Where(p => p.Status == request.status.Value);
            }

            if (request.categoryid.HasValue)
            {
                query = query.Where(p => p.CategoryID == request.categoryid.Value);
            }

            if (!string.IsNullOrEmpty(request.businessid))
            {
                query = query.Where(p => p.BusinessID == request.businessid);
            }

            if (request.SortOrder.HasValue)
            {
                // Apply sorting by price
                query = request.SortOrder.Value == true
                ? query.OrderByDescending(p => p.Price)
                : query.OrderBy(p => p.Price);
            }

            // Pagination
            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query.Skip((request.pagenumber - 1) * request.pagesize)
                                   .Take(request.pagesize)
                                   .ToListAsync(cancellationToken);
            var pageCount = totalCount / request.PageSize;
            if (pageCount % request.PageSize >= 1 || pageCount == 0)
            {
                pageCount++;
            }

            var dtos = _mapper.Map<List<ProductDTO>>(items);

            return new PagedResult<ProductDTO>
            {
                Data = dtos,
                TotalCount = totalCount,
                PageCount = pageCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
