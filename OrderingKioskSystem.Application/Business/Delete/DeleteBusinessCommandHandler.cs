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

namespace OrderingKioskSystem.Application.Business.Delete
{
    public class DeleteBusinessCommandHandler : IRequestHandler<DeleteBusinessCommand, string>, ICommand
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IMapper _mapper;
        private ICurrentUserService _currentUserService;
        public DeleteBusinessCommandHandler(IBusinessRepository businessRepository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _businessRepository = businessRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(DeleteBusinessCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("User ID not exist .");
            }
            var existbusiness = await _businessRepository.FindAsync(x => x.ID == request.Id, cancellationToken);
            if (existbusiness == null)
            {
                throw new NotFoundException($"Business with Id  {request.Id} not found .");
            }

            if (existbusiness.NgayXoa != null)
            {
                return "Business has been deleted.";
            }
            existbusiness.NguoiXoaID = userId;
            existbusiness.NgayXoa = DateTime.Now;
            _businessRepository.Update(existbusiness);
            await _businessRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return "Delete business success.";
        }
    }
}
