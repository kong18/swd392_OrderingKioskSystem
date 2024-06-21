using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
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
        private readonly ICurrentUserService _currentUserService;

        public CreateProductCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IBusinessRepository businessRepository, FileUploadService fileUploadService, ICurrentUserService currentUserService)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _businessRepository = businessRepository;
            _fileUploadService = fileUploadService;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var categoryExist = await _categoryRepository.FindAsync(x => x.ID == request.CategoryID && !x.NgayXoa.HasValue, cancellationToken);

            if (categoryExist is null)
            {
                throw new NotFoundException("Category does not exist");
            }

            var businessID = _currentUserService.UserId;

            bool businessExist = await _businessRepository.AnyAsync(x => x.ID == businessID && !x.NgayXoa.HasValue, cancellationToken);

            if (!businessExist)
            {
                throw new NotFoundException("Business does not exist");
            }

            bool productExists = await _productRepository.AnyAsync(
                x => x.Name == request.Name && x.BusinessID == businessID && !x.NgayXoa.HasValue, cancellationToken);

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

            // Generate the product code based on the category
            string productCode = GenerateProductCode(categoryExist.Name);

            var p = new ProductEntity
            {
                Name = request.Name,
                Url = imageUrl, // Set the URL to the uploaded image URL
                Description = request.Description,
                Code = productCode,
                Price = request.Price,
                Status = request.Status,
                CategoryID = categoryExist.ID,
                BusinessID = businessID,
                NgayTao = DateTime.UtcNow.AddHours(7)
            };

            _productRepository.Add(p);

            return await _productRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Create Success!" : "Create Fail!";
        }

        private string GenerateProductCode(string categoryName)
        {
            string prefix = categoryName.ToLower() switch
            {
                "food" => "F",
                "drink" => "D",
                _ => "X" // Default prefix if the category is not recognized
            };

            // Generate a random 5-digit number
            Random random = new Random();
            int randomNumber = random.Next(10000, 99999);

            return $"{prefix}{randomNumber}";
        }
    }
}
