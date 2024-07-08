﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingKioskSystem.Application.Common.Pagination;
using OrderingKioskSystem.Infrastructure.Persistence;
using OrderingKioskSystem.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace OrderingKioskSystem.Application.Product.Filter
{
    public class FilterProductQueryHandler : IRequestHandler<FilterProductQuery, PagedResult<ProductDTO>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public FilterProductQueryHandler(ApplicationDbContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<PagedResult<ProductDTO>> Handle(FilterProductQuery request, CancellationToken cancellationToken)
        {
            // Cache key
            var cacheKey = $"FilterProductQuery_{request.PageNumber}_{request.PageSize}_{request.Name}_{request.Code}_{request.Status}_{request.CategoryID}_{request.BusinessID}_{request.SortOrder}";

            if (!_cache.TryGetValue(cacheKey, out PagedResult<ProductDTO> cachedProducts))
            {
                // If not found in cache, retrieve from database
                var query = _context.Products
                    .Include(p => p.Business)
                    .AsQueryable();

                query = query.OrderByDescending(x => x.NgayTao);

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

                if (request.SortOrder.HasValue)
                {
                    // Apply sorting by price
                    query = request.SortOrder.Value == true
                    ? query.OrderByDescending(p => p.Price)
                    : query.OrderBy(p => p.Price);
                }

                // Pagination
                var totalCount = await query.CountAsync(cancellationToken);
                var items = await query.Skip((request.PageNumber - 1) * request.PageSize)
                                    .Take(request.PageSize)
                                    .ToListAsync(cancellationToken);
                var pageCount = (int)Math.Ceiling((double)totalCount / request.PageSize);

                var dtos = _mapper.Map<List<ProductDTO>>(items);

                cachedProducts = new PagedResult<ProductDTO>
                {
                    Data = dtos,
                    TotalCount = totalCount,
                    PageCount = pageCount,
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize
                };

                // Set cache options
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    // thời gian store cache trong memory 
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60),
                    //thời gian xóa kể từ lần truy cập cuối 
                    SlidingExpiration = TimeSpan.FromMinutes(15)
                };

                // Save data in cache
                _cache.Set(cacheKey, cachedProducts, cacheEntryOptions);
            }

            return cachedProducts;
        }
    }
}
