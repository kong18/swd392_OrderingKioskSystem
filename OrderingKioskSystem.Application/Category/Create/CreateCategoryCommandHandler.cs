using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Application.FileUpload;
using OrderingKioskSystem.Application.Product.Create;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using OrderingKioskSystem.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Category.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, string>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly FileUploadService _fileUploadService;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, ICurrentUserService currentUserService, FileUploadService fileUploadService)
        {
            _categoryRepository = categoryRepository;
            _currentUserService = currentUserService;
            _fileUploadService = fileUploadService;
        }

        public async Task<string> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            bool categoryExist = await _categoryRepository.AnyAsync(x => x.Name == request.Name, cancellationToken);

            if (categoryExist)
            {
                return "Category's Name can't be dupplicated";
            }

            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("User ID không tìm thấy.");
            }

            string imageUrl = string.Empty;
            using (var stream = request.ImageFile.OpenReadStream())
            {
                imageUrl = await _fileUploadService.UploadFileAsync(stream, $"{Guid.NewGuid()}.jpg");
            }

            var category = new CategoryEntity
            {
                Name = request.Name,
                Url = imageUrl,

                NguoiTaoID = _currentUserService.UserId,
                NgayTao = DateTime.UtcNow.AddHours(7)
            };

            _categoryRepository.Add(category);

            return await _categoryRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Create Success!" : "Create Fail!";
        }
    }
}
