using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Business.GetBusinessById;
using OrderingKioskSystem.Application.Business;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Shipper.GetShipperById
{
    public class GetShipperByIdQueryHandler : IRequestHandler<GetShipperByIdQuery, ShipperDTO>
    {
        private readonly IMapper _mapper;
        private readonly IShipperRepository _repository;
        public GetShipperByIdQueryHandler(IMapper mapper, IShipperRepository shipperRepository)
        {
            _mapper = mapper;
            _repository = shipperRepository;
        }

        public async Task<ShipperDTO> Handle(GetShipperByIdQuery request, CancellationToken cancellationToken)
        {
            var existedShipper = await _repository.FindAsync(x => x.ID == request.Id, cancellationToken);
            if (existedShipper == null)
            {
                throw new NotFoundException($"Shipper with Id: {request.Id} not found .");
            }

            if (existedShipper.NgayXoa != null)
            {
                throw new NotFoundException($"Shipper với Id  {request.Id} has been deleted");
            }
            return _mapper.Map<ShipperDTO>(existedShipper);
        }
    }
}
