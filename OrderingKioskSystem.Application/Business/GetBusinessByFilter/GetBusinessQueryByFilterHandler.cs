using AutoMapper;
using MediatR;
using OrderingKioskSystem.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Business.GetAllBusiness
{
    public class GetBusinessQueryByFilterHandler : IRequestHandler<GetBusinessByFilterQuery, List<BusinessDTO>>
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IMapper _mapper;

        public GetBusinessQueryByFilterHandler(IBusinessRepository businessRepository, IMapper mapper)
        {
            _businessRepository = businessRepository;
            _mapper = mapper;
        }

        public async Task<List<BusinessDTO>> Handle(GetBusinessByFilterQuery request, CancellationToken cancellationToken)
        {
            var businesses = await _businessRepository.FindAllAsync(c => c.NgayXoa == null, cancellationToken);

            if (!string.IsNullOrEmpty(request.Name))
            {
                businesses = businesses.Where(b => b.Name.Contains(request.Name)).ToList();
            }

            if (!string.IsNullOrEmpty(request.BankName))
            {
                businesses = businesses.Where(b => b.BankName.Contains(request.BankName)).ToList();
            }

            return businesses.MapToBusinessDTOList(_mapper);
        }
    }
}
