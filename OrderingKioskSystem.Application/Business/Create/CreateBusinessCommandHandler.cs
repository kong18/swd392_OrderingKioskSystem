using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Business.GetAllBusiness;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Application.User.SendEmail;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using OrderingKioskSystem.Infrastructure.Repositories;

namespace OrderingKioskSystem.Application.Business.CreateBusinessCommand
{
    public class CreateBusinessCommandHandler : IRequestHandler<CreateBusinessCommand, string>
    {
        private readonly IBusinessRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

      public CreateBusinessCommandHandler(IBusinessRepository repository, ICurrentUserService currentUserService, IUserRepository userRepository)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
            _emailService = new EmailService();
        }

        public async Task<string> Handle(CreateBusinessCommand request, CancellationToken cancellationToken)
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
                Password = hashedPassword,
                Role = "Business"
            };

            

            _userRepository.Add(user);
            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            var email = user.Email;

            var business = new BusinessEntity
            {
                Email = email,
                NguoiTaoID = _currentUserService.UserId,
                Url = request.Url,
                Name = request.Name,
                BankName = request.BankName,
                BankAccountName = request.BankAccountName,
                BankAccountNumber = request.BankAccountNumber,

            };
            _repository.Add(business);
            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return "Create Business successfully";
        }
    }
}
