using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Application.User.Authenticate;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.User.Commands
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IBusinessRepository _businessRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly IJwtService _jwtService;

        public GoogleLoginCommandHandler(
            IUserRepository userRepository,
            IBusinessRepository businessRepository,
            IManagerRepository managerRepository,
            IJwtService jwtService)
        {
            _userRepository = userRepository;
            _businessRepository = businessRepository;
            _managerRepository = managerRepository;
            _jwtService = jwtService;
        }

        public async Task<string> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(x => x.Email == request.Email);
            if (user == null)
            {
                var password = _userRepository.GeneratePassword();
                // Register the user if they don't exist
                user = new UserEntity
                {
                    Email = request.Email,
                    Password = _userRepository.HashPassword(password),
                    Role = request.Role
                };

                if (request.Role == "Business")
                {
                    var business = new BusinessEntity
                    {
                        Email = request.Email,
                        BinId =00000000,
                        Name = "Default Business Name",
                        BankAccountNumber = "0000000000",
                        BankAccountName = "Default Bank Account",
                        BankName = "Default Bank",
                        Url = "http://default.url"
                    };
                    user.Business = business;
                    _businessRepository.Add(business);
                }
                else if (request.Role == "Manager")
                {
                    var manager = new ManagerEntity
                    {
                        Email = request.Email,
                        Name = "Default Manager Name",
                        Phone = "0000000000"
                    };
                    user.Manager = manager;
                    _managerRepository.Add(manager);
                }

                _userRepository.Add(user);
                await _userRepository.UnitOfWork.SaveChangesAsync(CancellationToken.None);
            }

            var jwtToken = _jwtService.CreateToken(user.Email, user.Role);
            return jwtToken;
        }
    }
}
