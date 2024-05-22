using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Kiosk.Delete
{
    public class DeleteKioskCommandHandler : IRequestHandler<DeleteKioskCommand, string>
    {
        private readonly IKioskRepository _repo;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public DeleteKioskCommandHandler(IKioskRepository repo, IMapper mapper, ICurrentUserService currentUserService)
        {
            _repo = repo;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(DeleteKioskCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("User ID not found.");
            }


            var entity = await _repo.FindAsync(x => x.ID == request.Id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException($"Kiosk with ID {request.Id} not found.");
            }

            if (entity.NgayXoa != null)
            {
                throw new NotFoundException("Id already deleted ");
            }

            entity.NguoiXoaID = userId;
            entity.NgayXoa = DateTime.Now;
            _repo.Update(entity);
            await _repo.UnitOfWork.SaveChangesAsync(cancellationToken);

            return "Delete kiosk successfully";
        }
    }
}
