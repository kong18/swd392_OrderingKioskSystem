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

namespace OrderingKioskSystem.Application.Kiosk.Update
{
    public class UpdateKioskCommandHandler : IRequestHandler<UpdateKioskCommand, string>
    {
        private readonly IKioskRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public UpdateKioskCommandHandler(IKioskRepository repository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(UpdateKioskCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("User ID not found.");
            }

            var existingKiosk = await _repository.FindAsync(x => x.ID == request.Id, cancellationToken);
            if (existingKiosk == null)
            {
                throw new NotFoundException($"Kiosk with Id {request.Id} not found.");
            }

            if (existingKiosk.NgayXoa != null)
            {
                throw new NotFoundException("This ID has been deleted");
            }

            existingKiosk.NguoiCapNhatID = userId;
            existingKiosk.Location = request.location;      
            existingKiosk.NgayCapNhatCuoi = DateTime.UtcNow.AddHours(7);

            _repository.Update(existingKiosk);
            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return "Update Kiosk successfully.";
        }
    }
}
