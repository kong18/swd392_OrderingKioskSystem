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

namespace OrderingKioskSystem.Application.Business.Update
{
    public class UpdateBusinessCommandHandler :IRequestHandler<UpdateBusinessCommand, string>
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public UpdateBusinessCommandHandler(IBusinessRepository businessRepository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _businessRepository = businessRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(UpdateBusinessCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("User ID does not exist.");
            }

            var existingBusiness = await _businessRepository.FindAsync(x => x.ID == request.Id, cancellationToken);
            if (existingBusiness == null)
            {
                throw new NotFoundException($"Business with Id {request.Id} not found.");
            }
            existingBusiness.NguoiCapNhatID = userId;
            existingBusiness.Url = request.Url;
            existingBusiness.Name = request.Name;
            existingBusiness.BankAccountNumber = request.BankAccountNumber;
            existingBusiness.BankAccountName = request.BankAccountName;
            existingBusiness.BankName = request.BankName;
            existingBusiness.NgayCapNhatCuoi = DateTime.UtcNow;
           
            _businessRepository.Update(existingBusiness);
            await _businessRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return "Update business success.";
        }
    }
}
