using MediatR;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.User.CreateManager
{
    public class CreateManagerUserCommandHandler : IRequestHandler<CreateManagerUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IManagerRepository _managerRepository;

        public CreateManagerUserCommandHandler(
            IUserRepository userRepository,
            IManagerRepository managerRepository)
        {
            _userRepository = userRepository;
            _managerRepository = managerRepository;
        }

        public async Task<string> Handle(CreateManagerUserCommand request, CancellationToken cancellationToken)
        {
            var user = new UserEntity
            {
                Email = request.Email,
                Password = request.Password,
                Role = request.Role
            };

            var manager = new ManagerEntity
            {
                Name = request.ManagerName,
                Phone = request.Phone,
                Email = request.Email,
                User = user
            };

            _managerRepository.Add(manager);
            _userRepository.Add(user);
            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return user.Email;
        }
    }
}
