using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Business.Update;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Repositories;
using OrderingKioskSystem.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Shipper.UpdateShipper
{
    public class UpdateShipperCommandHandler : IRequestHandler<UpdateShipperCommand, string>
    {
        private readonly IShipperRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public UpdateShipperCommandHandler(IShipperRepository repository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(UpdateShipperCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("User ID does not exist.");
            }

            var existingShipper = await _repository.FindAsync(x => x.ID == request.Id, cancellationToken);
            if (existingShipper == null)
            {
                throw new NotFoundException($"Shipper with Id {request.Id} not found.");
            }
            existingShipper.NguoiCapNhatID = userId;
            existingShipper.Name = request.Name ?? existingShipper.Name;
            existingShipper.Phone = request.Phone ?? existingShipper.Phone;
            existingShipper.Address = request.Address ?? existingShipper.Address;
            
           
            existingShipper.NgayCapNhatCuoi = DateTime.UtcNow;

            _repository.Update(existingShipper);
            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return "Update shipper success.";
        }
    }
}
