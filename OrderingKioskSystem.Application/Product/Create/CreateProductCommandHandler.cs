using MediatR;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                return "CategoryID does not exist";
            }

            bool businessExist = await _businessRepository.AnyAsync(x => x.ID == request.BusinessID && !x.NgayXoa.HasValue, cancellationToken);

            if (!businessExist)
            {
                return "BusinessID does not exist";
            }

            var p = new ProductEntity
            {
                Name = request.Name,
                Url = request.Url,
                Description = request.Description,
                Code = request.Code,
                Price = request.Price,
                IsAvailable = request.IsAvailable,
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
