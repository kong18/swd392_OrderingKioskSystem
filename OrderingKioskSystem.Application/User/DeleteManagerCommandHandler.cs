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

namespace SWD.OrderingKioskSystem.Application.User
{
    public class DeleteManagerCommandHandler : IRequestHandler<DeleteManagerCommand, string>
    {
        private readonly IManagerRepository _repository;
        private readonly IMapper _mapper;
        private ICurrentUserService _currentUserService;
        public DeleteManagerCommandHandler(IManagerRepository repository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(DeleteManagerCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("User ID not exist .");
            }
            var existmanager = await _repository.FindAsync(x => x.ID == request.Id, cancellationToken);
            if (existmanager == null)
            {
                throw new NotFoundException($"Manager with Id  {request.Id} not found .");
            }

            if (existmanager.NgayXoa != null)
            {
                return "Manager has been deleted.";
            }



            existmanager.NguoiXoaID = userId;
            existmanager.NgayXoa = DateTime.Now;
            _repository.Update(existmanager);
            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return "Delete Manager success.";
        
    }
    }
}
