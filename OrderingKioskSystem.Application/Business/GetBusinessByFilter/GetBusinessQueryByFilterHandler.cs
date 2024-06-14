using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Common.Pagination;
using OrderingKioskSystem.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Business.GetBusinessByFilter
{
    public class GetBusinessQueryByFilterHandler : IRequestHandler<GetBusinessByFilterQuery, PagedResult<BusinessDTO>>
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IMapper _mapper;

        public GetBusinessQueryByFilterHandler(IBusinessRepository businessRepository, IMapper mapper)
        {
            _businessRepository = businessRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<BusinessDTO>> Handle(GetBusinessByFilterQuery request, CancellationToken cancellationToken)
        {
            var businesses = await _businessRepository.FindAllAsync(c => c.NgayXoa == null, cancellationToken);

            // Apply filtering
            if (!string.IsNullOrEmpty(request.Name))
            {
                businesses = businesses.Where(b => b.Name.ToLower().Contains(request.Name.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(request.BankName))
            {
                businesses = businesses.Where(b => b.BankName.ToLower().Contains(request.BankName.ToLower())).ToList();
            }

            // Convert filtered list to IQueryable for pagination
            var query = businesses.AsQueryable();

            // Pagination
            var totalCount = query.Count();
            var items = query.Skip((request.PageNumber - 1) * request.PageSize)
                             .Take(request.PageSize)
                             .ToList();

            var dtos = _mapper.Map<List<BusinessDTO>>(items);

            return new PagedResult<BusinessDTO>
            {
                Data = dtos,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
