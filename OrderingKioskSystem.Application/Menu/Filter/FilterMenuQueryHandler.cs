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

namespace OrderingKioskSystem.Application.Menu.Filter
{
    public class FilterMenuQueryHandler : IRequestHandler<FilterMenuQuery, PagedResult<MenuDTO>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public FilterMenuQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<PagedResult<MenuDTO>> Handle(FilterMenuQuery request, CancellationToken cancellationToken)
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

            var dtos = _mapper.Map<List<MenuDTO>>(items);

            return new PagedResult<MenuDTO>
            {
                Data = dtos,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
