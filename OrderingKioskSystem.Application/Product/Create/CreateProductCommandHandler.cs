using MediatR;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Product.Create
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, string>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBusinessRepository _businessRepository;

        public CreateProductCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IBusinessRepository businessRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _businessRepository = businessRepository;
        }

        public async Task<string> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            bool categoryExist = await _categoryRepository.AnyAsync(x => x.ID == request.CategoryID && !x.NgayXoa.HasValue, cancellationToken);

            if (!categoryExist)
            {
                throw new NotFoundException("CategoryId does not exist");
            }

            bool businessExist = await _businessRepository.AnyAsync(x => x.ID == request.BusinessID && !x.NgayXoa.HasValue, cancellationToken);

            if (!businessExist)
            {
                throw new NotFoundException("Business does not exist");
            }

            bool productExists = await _productRepository.AnyAsync(
                x => x.Name == request.Name && x.BusinessID == request.BusinessID && !x.NgayXoa.HasValue, cancellationToken);

            if (productExists)
            {
                throw new DuplicationException("Same product has been existed");
            }

            var p = new ProductEntity
            {
                Name = request.Name,
                Url = request.Url,
                Description = request.Description,
                Code = request.Code,
                Price = request.Price,
                Status = request.Status,
                CategoryID = request.CategoryID,
                BusinessID = request.BusinessID,
                NgayTao = DateTime.Now
            };

            _productRepository.Add(p);

            return await _productRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Create Success!" : "Create Fail!";
        }
    }
}
