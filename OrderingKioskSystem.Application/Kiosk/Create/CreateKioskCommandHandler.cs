using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Kiosk.Create
{
    public class CreateKioskCommandHandler : IRequestHandler<CreateKioskCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly IKioskRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        public CreateKioskCommandHandler(IMapper mapper, IKioskRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<string> Handle(CreateKioskCommand request, CancellationToken cancellationToken)
        {
            //var userId = _currentUserService.UserId;
            //if (string.IsNullOrEmpty(userId))
            //{
            //    throw new UnauthorizedAccessException("User ID không tìm thấy.");
            //}

            var kiosk = new KioskEntity
            {
                NguoiTaoID = _currentUserService?.UserId,
                Location = request.location
            };

            _repository.Add(kiosk);
            if (await _repository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0)
                return "Tạo thành công";
            else
                return "Tạo thất bại";
        }

    }
}
