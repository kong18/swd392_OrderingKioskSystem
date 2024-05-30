using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Shipper.CreateShipper
{
    public class CreateShipperCommandHandler : IRequestHandler<CreateShipperCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly IShipperRepository _shipperRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;

        public CreateShipperCommandHandler(IMapper mapper, IProductRepository productRepository, ICurrentUserService currentUserService, IUserRepository userRepository, IShipperRepository shipperRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
            _shipperRepository = shipperRepository;
        }

        public async Task<string> Handle(CreateShipperCommand request, CancellationToken cancellationToken)
        {
            var hashedPassword = _userRepository.HashPassword(request.Password);
            var user = new UserEntity
            {
                Email = request.Email,
                Password = hashedPassword,
                Role = "Shipper"
            };

            _userRepository.Add(user);
            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            var email = user.Email;

            var shipper = new ShipperEntity
            {
                Email = email,
                NguoiTaoID = _currentUserService.UserId,
                Name = request.ShipperName,
                Phone = request.Phone,
                Address = request.Address,
                User = user
            };
            _shipperRepository.Add(shipper);
            await _shipperRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return "Create Shipper successfully";
        }
    }
}
