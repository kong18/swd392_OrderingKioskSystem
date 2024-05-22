using AutoMapper;
using MediatR;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Business.GetAllBusiness
{
    public class GetAllBusinessQueryHandler : IRequestHandler<GetAllBusinessQuery, List<BusinessDTO>>
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IMapper _mapper;
        public GetAllBusinessQueryHandler(IBusinessRepository businessRepository, IMapper mapper)
        {
            _businessRepository = businessRepository;
            _mapper = mapper;
        }
    
        public async Task<List<BusinessDTO>> Handle(GetAllBusinessQuery request, CancellationToken cancellationToken)
        {
          var business = await _businessRepository.FindAllAsync(c=> c.NgayXoa == null,cancellationToken);
            return business.MapToBusinessDTOList(_mapper);
        }
    }
}
