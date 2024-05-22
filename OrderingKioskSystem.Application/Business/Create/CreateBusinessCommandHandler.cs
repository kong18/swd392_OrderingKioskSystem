using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Business.CreateBusinessCommand
{
    public class CreateBusinessCommandHandler : IRequestHandler<CreateBusinessCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly IBusinessRepository _repository;
       private readonly IMenuRepository _menuRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICurrentUserService _currentUserService;
      public CreateBusinessCommandHandler(IMapper mapper, IBusinessRepository repository, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(CreateBusinessCommand request, CancellationToken cancellationToken)
        {
            var business = new BusinessEntity
            {
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
