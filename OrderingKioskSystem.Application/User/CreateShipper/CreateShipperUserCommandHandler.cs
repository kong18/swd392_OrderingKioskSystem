using MediatR;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.User.CreateShipper
{
    public class CreateShipperUserCommandHandler : IRequestHandler<CreateShipperUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IShipperRepository _shipperRepository;

        public CreateShipperUserCommandHandler(
            IUserRepository userRepository,
            IShipperRepository shipperRepository)
        {
            _userRepository = userRepository;
            _shipperRepository = shipperRepository;
        }

        public async Task<string> Handle(CreateShipperUserCommand request, CancellationToken cancellationToken)
        {
            var user = new UserEntity
            {
                Email = request.Email,
                Password = request.Password,
                Role = request.Role
            };

            var shipper = new ShipperEntity
            {
                Email = request.Email,
                Name = request.ShipperName,
                Phone = request.Phone,
                Address = request.Address,
                User = user
            };

            _shipperRepository.Add(shipper);
            _userRepository.Add(user);
            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return user.Email;
        }
    }
}
