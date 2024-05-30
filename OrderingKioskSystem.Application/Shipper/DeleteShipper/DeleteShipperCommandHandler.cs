using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Business.Delete;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Shipper.DeleteShipper
{
    public class DeleteShipperCommandHandler : IRequestHandler<DeleteShipperCommand, string>, ICommand
    {
        private readonly IShipperRepository _repository;
        private readonly IMapper _mapper;
        private ICurrentUserService _currentUserService;
       
        public DeleteShipperCommandHandler(IShipperRepository shipperRepository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _repository = shipperRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
           
        }

        public async Task<string> Handle(DeleteShipperCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("User ID not exist .");
            }
            var existshipper = await _repository.FindAsync(x => x.ID == request.Id, cancellationToken);
            if (existshipper == null)
            {
                throw new NotFoundException($"Shipper with Id  {request.Id} not found .");
            }

            if (existshipper.NgayXoa != null)
            {
                return "Shipper has been deleted.";
            }



            existshipper.NguoiXoaID = userId;
            existshipper.NgayXoa = DateTime.Now;
            _repository.Update(existshipper);
            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return "Delete Shipper success.";
        }
    }
}
