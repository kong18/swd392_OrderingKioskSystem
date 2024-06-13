using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingKioskSystem.Application.Common.Pagination;
using OrderingKioskSystem.Application.Product;
using OrderingKioskSystem.Domain.Repositories;
using OrderingKioskSystem.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Business.GetAllBusiness
{
    public class GetBusinessQueryByFilterHandler : IRequestHandler<GetBusinessByFilterQuery, PagedResult<BusinessDTO>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetBusinessQueryByFilterHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResult<BusinessDTO>> Handle(GetBusinessByFilterQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Business.AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(p => p.Name.Contains(request.Name));
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                query = query.Where(p => p.Email.Contains(request.Email));
            }

            if (!string.IsNullOrEmpty(request.BankName))
            {
                query = query.Where(p => p.BankName.Contains(request.BankName));
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
            var dtos = _mapper.Map<List<BusinessDTO>>(items);

            return new PagedResult<BusinessDTO>
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
