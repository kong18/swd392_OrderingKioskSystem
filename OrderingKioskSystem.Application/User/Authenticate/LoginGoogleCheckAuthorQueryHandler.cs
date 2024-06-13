using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Application.User;
using OrderingKioskSystem.Application.User.Authenticate;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.User.Authenticate
{
    public class LoginGoogleCheckAuthorQueryHandler : IRequestHandler<LoginGoogleCheckAuthorQuery, UserLoginDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public LoginGoogleCheckAuthorQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserLoginDTO> Handle(LoginGoogleCheckAuthorQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(x => x.Email == request.Email);

            var userLoginDto = _mapper.Map<UserLoginDTO>(user);

            if (user?.Business != null)
            {
                userLoginDto.EntityId = user.Business.ID;
                userLoginDto.Role = "Business";
            }
            else if (user?.Manager != null)
            {
                userLoginDto.EntityId = user.Manager.ID;
                userLoginDto.Role = "Manager";
            }
            else if (user?.Shipper != null)
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
