﻿using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Domain.Repositories;
using OrderingKioskSystem.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Kiosk.GetAll
{
    public class GetAllKioskQueryHandler : IRequestHandler<GetAllKioskQuery, List<KioskDTO>> , IQuery  
    {
        private readonly IKioskRepository _repository;
        private readonly IMapper _mapper;   

        public GetAllKioskQueryHandler(KioskRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<KioskDTO>> Handle(GetAllKioskQuery request, CancellationToken cancellationToken)
        {
            var ctnp = await _repository.FindAllAsync(c => c.NgayXoa == null, cancellationToken);
            return ctnp.MapToKioskDTOList(_mapper);
        }

       
    }
}