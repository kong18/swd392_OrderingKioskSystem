using MediatR;
using OrderingKioskSystem.Application.Product.Create;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Product.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, string>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBusinessRepository _businessRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IBusinessRepository businessRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _businessRepository = businessRepository;
        }

        public async Task<string> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productExist = await _productRepository.FindAsync(x => x.ID == request.ID, cancellationToken);

            if (productExist is null || productExist.NgayXoa.HasValue)
            {
                return "ProductID does not exist";
            }

            
            bool categoryExist = await _categoryRepository.AnyAsync(x => x.ID == request.CategoryID, cancellationToken);

            if (!categoryExist)
            {
                return "CategoryID does not exist";
            }
            

            
            bool businessExist = await _businessRepository.AnyAsync(x => x.ID == request.BusinessID, cancellationToken);

            if (!businessExist)
            {
                return "BusinessID does not exist";
            }
            

            productExist.Name = request.Name;
            productExist.Code = request.Code;
            productExist.Url = request.Url;
            productExist.Description = request.Description;
            productExist.Price = request.Price;
            productExist.IsAvailable = request.IsAvailable;
            productExist.Status = request.Status;
            productExist.CategoryID = request.CategoryID;
            productExist.BusinessID = request.BusinessID;
            
            productExist.NgayCapNhatCuoi = DateTime.Now;
            _productRepository.Update(productExist);

            return await _productRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Update Success!" : "Update Fail!";
        }
    }
}
