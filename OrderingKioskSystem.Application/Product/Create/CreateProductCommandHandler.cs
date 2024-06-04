using MediatR;
using OrderingKioskSystem.Application.FileUpload;
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
        private readonly FileUploadService _fileUploadService;

        public CreateProductCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IBusinessRepository businessRepository, FileUploadService fileUploadService)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _businessRepository = businessRepository;
            _fileUploadService = fileUploadService;
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

            // Upload the image and get the URL
            string imageUrl = string.Empty;
            if (request.ImageFile != null)
            {
                using (var stream = request.ImageFile.OpenReadStream())
                {
                    imageUrl = await _fileUploadService.UploadFileAsync(stream, $"{Guid.NewGuid()}.jpg");
                }
            }

            var p = new ProductEntity
            {
                Name = request.Name,
                Url = imageUrl, // Set the URL to the uploaded image URL
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
