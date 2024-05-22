using AutoMapper;
using MediatR;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Kiosk.GetById
{
    public class GetKioskByIdHandler : IRequestHandler<GetKioskByIdQuery,KioskDTO>
    {
        private readonly IKioskRepository _repository;
        private readonly IMapper _mapper;
        public GetKioskByIdHandler(IKioskRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<KioskDTO> Handle(GetKioskByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.FindAsync(x => x.ID == request.Id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException($"Entity with ID {request.Id} not found");
            }
            if (entity.NgayXoa != null)
            {
                throw new NotFoundException($"Entity with ID {request.Id} has been deleted");
            }

            return _mapper.Map<KioskDTO>(entity);
        }
    }
}
