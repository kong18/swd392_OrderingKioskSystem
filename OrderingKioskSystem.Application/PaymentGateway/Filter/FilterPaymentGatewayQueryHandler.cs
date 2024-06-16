using AutoMapper;
using OrderingKioskSystem.Application.Common.Pagination;
using OrderingKioskSystem.Application.Product.Filter;
using OrderingKioskSystem.Application.Product;
using OrderingKioskSystem.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SWD.OrderingKioskSystem.Application.PaymentGateway.Filter
{
    public class FilterPaymentGatewayQueryHandler : IRequestHandler<FilterPaymentGatewayQuery, PagedResult<PaymentGatewayDTO>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FilterPaymentGatewayQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResult<PaymentGatewayDTO>> Handle(FilterPaymentGatewayQuery request, CancellationToken cancellationToken)
        {
            var query = _context.PaymentGateways.AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(p => p.Name.Contains(request.Name));
            }

            query = query.OrderBy(p => p.Name);

            // Pagination
            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query.Skip((request.PageNumber - 1) * request.PageSize)
                                   .Take(request.PageSize)
                                   .ToListAsync(cancellationToken);

            var pageCount = (int)Math.Ceiling((double)totalCount / request.PageSize);

            var dtos = _mapper.Map<List<PaymentGatewayDTO>>(items);

            return new PagedResult<PaymentGatewayDTO>
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
