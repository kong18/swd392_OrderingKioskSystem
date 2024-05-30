using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.User.Authenticate
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, UserLoginDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;

        public LoginQueryHandler(IUserRepository userRepository, IMapper mapper, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public async Task<UserLoginDTO> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(x => x.Email == request.Email);

            if (user == null || !_userRepository.VerifyPassword(request.Password, user.Password))
            {
                throw new NotFoundException($"No user found with email - {request.Email} or incorrect password.");
            }

            var userLoginDto = _mapper.Map<UserLoginDTO>(user);

            if (user.Business != null)
            {
                userLoginDto.EntityId = user.Business.ID;
                userLoginDto.Role = "Business";
            }
            else if (user.Manager != null)
            {
                userLoginDto.EntityId = user.Manager.ID;
                userLoginDto.Role = "Manager";
            }
            else if (user.Shipper != null)
            {
                userLoginDto.EntityId = user.Shipper.ID;
                userLoginDto.Role = "Shipper";
            }
            else
            {
                throw new NotFoundException($"No associated entity found for user - {request.Email}");
            }

            return userLoginDto;
        }
    }
}
