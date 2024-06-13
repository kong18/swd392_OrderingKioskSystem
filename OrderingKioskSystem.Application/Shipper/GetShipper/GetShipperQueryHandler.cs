using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingKioskSystem.Application.Common.Pagination;
using Microsoft.EntityFrameworkCore;
using OrderingKioskSystem.Infrastructure.Persistence;

namespace OrderingKioskSystem.Application.Shipper.GetAllShipper
{
    public class GetAllShipperQueryHandler : IRequestHandler<GetShipperQuery, PagedResult<ShipperDTO>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllShipperQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResult<ShipperDTO>> Handle(GetShipperQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Shippers.AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(p => p.Name.Contains(request.Name));
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                query = query.Where(p => p.Email.Contains(request.Email));
            }

            if (!string.IsNullOrEmpty(request.Address))
            {
                query = query.Where(p => p.Address.Contains(request.Address));
            }

            if (!string.IsNullOrEmpty(request.Phone))
            {
                query = query.Where(p => p.Phone.Contains(request.Phone));
            }

            if (request.SortOrder.HasValue)
            {
                // Apply sorting by CreateDate
                query = request.SortOrder.Value == true
                ? query.OrderByDescending(p => p.NgayTao)
                : query.OrderBy(p => p.NgayTao);
            }

            // Pagination
            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query.Skip((request.PageNumber - 1) * request.PageSize)
                                   .Take(request.PageSize)
                                   .ToListAsync(cancellationToken);
            var pageCount = totalCount / request.PageSize;

            if (pageCount % request.PageSize >= 1 || pageCount == 0)
            {
                pageCount++;
            }
            var dtos = _mapper.Map<List<ShipperDTO>>(items);

            return new PagedResult<ShipperDTO>
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
