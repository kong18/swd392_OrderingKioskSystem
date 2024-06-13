using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingKioskSystem.Application.Common.Pagination;
using OrderingKioskSystem.Application.Kiosk;
using OrderingKioskSystem.Infrastructure.Persistence;
using SWD.OrderingKioskSystem.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Kiosk.Get
{
    public class SearchKioskQueryHandler : IRequestHandler<SearchKioskQuery, PagedResult<KioskDTO>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SearchKioskQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResult<KioskDTO>> Handle(SearchKioskQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Kiosk.AsQueryable();

            if (!string.IsNullOrEmpty(request.Location))
            {
                query = query.Where(k => k.Location.Contains(request.Location));
            }

           
            var pageSize = request.PageSize > 0 ? request.PageSize : PaginationDefaults.DefaultPageSize;
            pageSize = pageSize > PaginationDefaults.MaxPageSize ? PaginationDefaults.MaxPageSize : pageSize;

           
            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query.Skip((request.PageNumber - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync(cancellationToken);
            var pageCount = totalCount / request.PageSize;

            if (pageCount % request.PageSize >= 1 || pageCount == 0)
            {
                pageCount++;
            }

            var dtos = _mapper.Map<List<KioskDTO>>(items);

            return new PagedResult<KioskDTO>
            {
                Data = dtos,
                TotalCount = totalCount,
                PageCount = pageCount,
                PageNumber = request.PageNumber,
                PageSize = pageSize
            };
        }
    }
}
