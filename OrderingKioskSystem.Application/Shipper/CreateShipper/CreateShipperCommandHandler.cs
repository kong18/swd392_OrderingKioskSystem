using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Application.User.SendEmail;
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
        private readonly IEmailService _emailService;

        public CreateShipperCommandHandler(IMapper mapper, IProductRepository productRepository, ICurrentUserService currentUserService, IUserRepository userRepository, IShipperRepository shipperRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
            _shipperRepository = shipperRepository;
            _emailService = new EmailService();
        }

        public async Task<string> Handle(CreateShipperCommand request, CancellationToken cancellationToken)
        {
            bool userExist = await _userRepository.AnyAsync(x => x.Email == request.Email, cancellationToken);

            if (userExist)
            {
                return "User's email exists!";
            }

            SendMailModel model = new SendMailModel
            {
                ReceiveAddress = request.Email,
                Content = VerificationCodeGenerator.GenerateVerificationCode().ToString()
            };

            _emailService.SendMail(model);

            var hashedPassword = _userRepository.HashPassword(model.Content);
            var user = new UserEntity
            {
                Email = request.Email,
                Password = _userRepository.HashPassword(model.Content),
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
