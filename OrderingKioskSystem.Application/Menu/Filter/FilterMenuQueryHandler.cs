using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Common.Pagination;
using OrderingKioskSystem.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace OrderingKioskSystem.Application.Menu.Filter
{
    public class FilterMenuQueryHandler : IRequestHandler<FilterMenuQuery, PagedResult<MenuDTO>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly string _cacheKey = "FilterMenuQuery";

        public FilterMenuQueryHandler(ApplicationDbContext context, IMapper mapper, IMemoryCache memoryCache)
        {
            _mapper = mapper;
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task<PagedResult<MenuDTO>> Handle(FilterMenuQuery request, CancellationToken cancellationToken)
        {
            string cacheKey = $"{_cacheKey}_{request.PageNumber}_{request.PageSize}_{request.Name}_{request.Title}_{request.Type}_{request.Status}_{request.BusinessID}_{request.BusinessName}";

            if (!_memoryCache.TryGetValue(cacheKey, out PagedResult<MenuDTO> cachedResult))
            {
                var query = _context.Menus.AsQueryable();

                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(p => p.Name.ToLower().Contains(request.Name.ToLower()));
                }

                if (!string.IsNullOrEmpty(request.Title))
                {
                    query = query.Where(p => p.Title.ToLower().Contains(request.Title.ToLower()));
                }

                if (!string.IsNullOrEmpty(request.Type))
                {
                    query = query.Where(p => p.Type.ToLower().Contains(request.Type.ToLower()));
                }

                if (request.Status != null)
                {
                    query = query.Where(p => p.Status.Equals(request.Status));
                }

                if (!string.IsNullOrEmpty(request.BusinessID))
                {
                    query = query.Where(p => p.BusinessID.Contains(request.BusinessID));
                }

                if (!string.IsNullOrEmpty(request.BusinessName))
                {
                    query = query.Where(p => p.Business.Name.Contains(request.BusinessName));
                }

                query = query.Where(p => p.NgayXoa == null);

                // Pagination
                var totalCount = await query.CountAsync(cancellationToken);
                var items = await query.Skip((request.PageNumber - 1) * request.PageSize)
                                       .Take(request.PageSize)
                                       .ToListAsync(cancellationToken);
                var pageCount = (int)Math.Ceiling((double)totalCount / request.PageSize);

                var dtos = _mapper.Map<List<MenuDTO>>(items);

                cachedResult = new PagedResult<MenuDTO>
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
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };

                // Cache the result
                _memoryCache.Set(cacheKey, cachedResult, cacheEntryOptions);
            }

            return cachedResult;
        }
    }
}
