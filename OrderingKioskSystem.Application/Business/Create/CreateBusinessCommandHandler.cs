using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Business.GetAllBusiness;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using OrderingKioskSystem.Infrastructure.Repositories;

namespace OrderingKioskSystem.Application.Business.CreateBusinessCommand
{
    public class CreateBusinessCommandHandler : IRequestHandler<CreateBusinessCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly IBusinessRepository _repository;
        private readonly IMenuRepository _menuRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;
      public CreateBusinessCommandHandler(IMapper mapper, IBusinessRepository repository, ICurrentUserService currentUserService, IUserRepository userRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(CreateBusinessCommand request, CancellationToken cancellationToken)
        {
            var hashedPassword = _userRepository.HashPassword(request.Password);
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
                //NguoiTaoID = _currentUserService.UserId,
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
