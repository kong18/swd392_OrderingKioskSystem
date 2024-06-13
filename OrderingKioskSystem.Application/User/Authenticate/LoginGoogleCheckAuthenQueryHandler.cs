using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Application.User.Authenticate;
using OrderingKioskSystem.Application.User;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.User.Authenticate
{
    public class LoginGoogleCheckAuthenQueryHandler : IRequestHandler<LoginGoogleCheckAuthenQuery, bool>
    {
        private readonly IUserRepository _userRepository;

        public LoginGoogleCheckAuthenQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(LoginGoogleCheckAuthenQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.AnyAsync(x => x.Email == request.Email); ;
        }
    }
}
