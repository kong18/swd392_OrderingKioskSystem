using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingKioskSystem.Application.Common.Pagination;
using OrderingKioskSystem.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Order.Filter
{
    public class FilterOrderQueryHandler : IRequestHandler<FilterOrderQuery, PagedResult<OrderDTO>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public FilterOrderQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<PagedResult<OrderDTO>> Handle(FilterOrderQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Orders.AsQueryable();

            query.OrderByDescending(p => p.NgayTao);

            if (!string.IsNullOrEmpty(request.KioskID))
            {
                query = query.Where(p => p.KioskID.Contains(request.KioskID));
            }

            if (!string.IsNullOrEmpty(request.Location))
            {
                query = query.Where(p => p.Kiosk.Location.Contains(request.Location));
            }

            if (request.MinTotal.HasValue)
            {
                query = query.Where(p => p.Total >= request.MinTotal.Value);
            }

            if (request.MaxTotal.HasValue)
            {
                query = query.Where(p => p.Total <= request.MaxTotal.Value);
            }


            if (!string.IsNullOrEmpty(request.Status))
            {
                query = query.Where(p => p.Status.Contains(request.Status));
            }

            if (!string.IsNullOrEmpty(request.ShipperID))
            {
                query = query.Where(p => p.ShipperID.Contains(request.ShipperID));
            }

            if (!string.IsNullOrEmpty(request.ShipperName))
            {
                query = query.Where(p => p.Shipper.Name.Contains(request.ShipperName));
            }
            query = query.Where(p => p.NgayXoa == null);


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

            var dtos = _mapper.Map<List<OrderDTO>>(items);

            return new PagedResult<OrderDTO>
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
