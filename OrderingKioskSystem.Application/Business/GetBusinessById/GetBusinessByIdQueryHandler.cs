using AutoMapper;
using MediatR;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Business.GetBusinessById
{
    public class GetBusinessByIdQueryHandler : IRequestHandler<GetBusinessByIdQuery, BusinessDTO>
    {
        private readonly IMapper _mapper;
        private readonly IBusinessRepository _businessRepository;
        public GetBusinessByIdQueryHandler(IMapper mapper, IBusinessRepository businessRepository)
        {
            _mapper = mapper;
            _businessRepository = businessRepository;
        }
    
        public async Task<BusinessDTO> Handle(GetBusinessByIdQuery request, CancellationToken cancellationToken)
        {
            var existedbuisiness = await _businessRepository.FindAsync(x => x.ID == request.Id, cancellationToken); 
            if (existedbuisiness == null)
            {
                throw new NotFoundException($"Business with Id: {request.Id} not found .");
            }

            if (existedbuisiness.NgayXoa != null)
            {
                throw new NotFoundException($"Business với Id  {request.Id} has been deleted");
            }
            return _mapper.Map<BusinessDTO>(existedbuisiness);
        }
    }
}
