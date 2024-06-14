using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Application.FileUpload;
using OrderingKioskSystem.Application.User.SendEmail;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;

namespace OrderingKioskSystem.Application.Business.CreateBusinessCommand
{
    public class CreateBusinessCommandHandler : IRequestHandler<CreateBusinessCommand, string>
    {
        private readonly IBusinessRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly FileUploadService _fileUploadService;

        public CreateBusinessCommandHandler(IBusinessRepository repository, ICurrentUserService currentUserService, IUserRepository userRepository, FileUploadService fileUploadService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
            _emailService = new EmailService();
            _fileUploadService = fileUploadService;
        }

        public async Task<string> Handle(CreateBusinessCommand request, CancellationToken cancellationToken)
        {
            bool userExist = await _userRepository.AnyAsync(x => x.Email == request.Email, cancellationToken);

            if (userExist)
            {
                throw new DuplicationException("Same user has been existed");
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
                Role = "Business"
            };

            

            _userRepository.Add(user);
            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            var email = user.Email;

            string imageUrl = string.Empty;
            using (var stream = request.ImageFile.OpenReadStream())
            {
                imageUrl = await _fileUploadService.UploadFileAsync(stream, $"{Guid.NewGuid()}.jpg");
            }

            var business = new BusinessEntity
            {
                Email = email,
                // NguoiTaoID = _currentUserService.UserId,
                Url = imageUrl,
                Name = request.Name,
                BankName = request.BankName,
                BankAccountName = request.BankAccountName,
                BankAccountNumber = request.BankAccountNumber,
                BinId = request.BinId,

                NgayTao = DateTime.UtcNow.AddHours(7)

            };
            _repository.Add(business);
            

            return await _repository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Create Business successfully!" : "Create Business Failed!";
        }
    }
}
