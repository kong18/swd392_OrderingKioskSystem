using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Business.GetAllBusiness;
using OrderingKioskSystem.Application.Business;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Shipper.GetAllShipper
{
    public class GetAllShipperQueryHandler : IRequestHandler<GetShipperQuery, List<ShipperDTO>>
    {
        private readonly IShipperRepository _repository;
        private readonly IMapper _mapper;
        public GetAllShipperQueryHandler(IShipperRepository shipperRepository, IMapper mapper)
        {
            _repository = shipperRepository;
            _mapper = mapper;
        }

        public async Task<List<ShipperDTO>> Handle(GetShipperQuery request, CancellationToken cancellationToken)
        {
            var shipper = await _repository.FindAllAsync(c => c.NgayXoa == null, cancellationToken);
            return shipper.MapToShipperDTOist(_mapper);
        }
    }
}
