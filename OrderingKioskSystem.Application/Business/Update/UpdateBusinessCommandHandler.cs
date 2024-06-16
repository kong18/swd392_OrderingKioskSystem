using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Application.FileUpload;
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
        private readonly FileUploadService _fileUploadService;

        public UpdateBusinessCommandHandler(IBusinessRepository businessRepository, IMapper mapper, ICurrentUserService currentUserService, FileUploadService fileUploadService)
        {
            _businessRepository = businessRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _fileUploadService = fileUploadService;
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

            string imageUrl = string.Empty;
            if (request.ImageFile != null)
            {
                using (var stream = request.ImageFile.OpenReadStream())
                {
                    imageUrl = await _fileUploadService.UploadFileAsync(stream, $"{Guid.NewGuid()}.jpg");
                }
            }
            
            existingBusiness.Url = !string.IsNullOrEmpty(imageUrl) ? imageUrl : existingBusiness.Url;
            existingBusiness.Name = request.Name ?? existingBusiness.Name;
            existingBusiness.BankAccountNumber = request.BankAccountNumber ?? existingBusiness.BankAccountNumber;
            existingBusiness.BankAccountName = request.BankAccountName ?? existingBusiness.BankAccountName;
            existingBusiness.BankName = request.BankName ?? existingBusiness.BankName;

            existingBusiness.NguoiCapNhatID = userId;
            existingBusiness.NgayCapNhatCuoi = DateTime.UtcNow.AddHours(7);
           
            _businessRepository.Update(existingBusiness);
            await _businessRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return "Update business success.";
        }
    }
}
