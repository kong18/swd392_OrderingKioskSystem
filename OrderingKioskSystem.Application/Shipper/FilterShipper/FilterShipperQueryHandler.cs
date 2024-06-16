using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Common.Pagination;
using OrderingKioskSystem.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OrderingKioskSystem.Application.Shipper;
using Microsoft.EntityFrameworkCore;

namespace SWD.OrderingKioskSystem.Application.Shipper.FilterShipper
{
    public class FilterShipperQueryHandler : IRequestHandler<FilterShipperQuery, PagedResult<ShipperDTO>>
    {
        private readonly IShipperRepository _repository;
        private readonly IMapper _mapper;

        public FilterShipperQueryHandler(IShipperRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ShipperDTO>> Handle(FilterShipperQuery request, CancellationToken cancellationToken)
        {
            // Fetch all shippers that are not marked as deleted
            var shippers = await _repository.FindAllAsync(c => c.NgayXoa == null, cancellationToken);

            if (!string.IsNullOrEmpty(request.Name))
            {
                shippers = shippers.Where(s => s.Name.ToLower().Contains(request.Name.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(request.Id))
            {
                shippers = shippers.Where(s => s.ID == request.Id).ToList();
            }

            var query = shippers.AsQueryable();

            // Pagination
            var totalCount = query.Count();
            var items = query.Skip((request.PageNumber - 1) * request.PageSize)
                             .Take(request.PageSize)
                             .ToList();
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
