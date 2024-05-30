using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.User.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;

        public ChangePasswordCommandHandler(IUserRepository userRepository, ICurrentUserService currentUserService)
        {
            _userRepository = userRepository;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var userEmail = _currentUserService.UserEmail;
            var user = await _userRepository.FindAsync(x => x.Email == userEmail);

            if (user == null)
            {
                throw new NotFoundException($"User not found");
            }

            if (!_userRepository.VerifyPassword(request.OldPassword, user.Password))
            {
                throw new NotFoundException("Old password is incorrect");
            }

            user.Password = _userRepository.HashPassword(request.NewPassword);
            _userRepository.Update(user);

            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return "Password changed successfully";
        }
    }
}
