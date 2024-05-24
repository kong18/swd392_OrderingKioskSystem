using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Kiosk.GetById;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Repositories;
using OrderingKioskSystem.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Kiosk.GetAll
{
    //public class GetAllKioskQueryHandler : IRequestHandler<GetAllKioskQuery, List<KioskDTO>>
    //{
    //    private readonly IKioskRepository _repository;
    //    private readonly IMapper _mapper;

<<<<<<< HEAD
    //    public GetAllKioskQueryHandler(KioskRepository repository, IMapper mapper)
    //    {
    //        _repository = repository;
    //        _mapper = mapper;
    //    }
=======
        public GetAllKioskQueryHandler(IKioskRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
>>>>>>> e05268a07ff5b906745a452c411855b3bd7dd665

    //    public async Task<List<KioskDTO>> Handle(GetAllKioskQuery request, CancellationToken cancellationToken)
    //    {
    //        var ctnp = await _repository.FindAllAsync(c => c.NgayXoa == null, cancellationToken);
    //        return ctnp.MapToKioskDTOList(_mapper);
    //    }
    //}
}
