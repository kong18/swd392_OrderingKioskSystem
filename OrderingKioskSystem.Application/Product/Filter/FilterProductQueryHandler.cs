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

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(p => p.Name.Contains(request.Name));
            }

            if (!string.IsNullOrEmpty(request.Code))
            {
                query = query.Where(p => p.Code.Contains(request.Code));
            }

            if (request.Status.HasValue)
            {
                query = query.Where(p => p.Status == request.Status.Value);
            }

            if (request.CategoryID.HasValue)
            {
                query = query.Where(p => p.CategoryID == request.CategoryID.Value);
            }

            if (!string.IsNullOrEmpty(request.BusinessID))
            {
                query = query.Where(p => p.BusinessID == request.BusinessID);
            }

            // Apply sorting by price
            query = request.SortOrder?.ToLower() == "desc"
                ? query.OrderByDescending(p => p.Price)
                : query.OrderBy(p => p.Price);

            // Pagination
            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query.Skip((request.PageNumber - 1) * request.PageSize)
                                   .Take(request.PageSize)
                                   .ToListAsync(cancellationToken);

            var dtos = _mapper.Map<List<ProductDTO>>(items);

            return new PagedResult<ProductDTO>
            {
                Data = dtos,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
