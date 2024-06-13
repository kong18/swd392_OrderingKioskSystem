using MediatR;
using OrderingKioskSystem.Application.Category.Create;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Application.FileUpload;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Category.Update
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, string>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly FileUploadService _fileUploadService;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, ICurrentUserService currentUserService, FileUploadService fileUploadService)
        {
            _categoryRepository = categoryRepository;
            _currentUserService = currentUserService;
            _fileUploadService = fileUploadService;
        }

        public async Task<string> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryExist = await _categoryRepository.FindAsync(x => x.ID == request.ID, cancellationToken);

            if (categoryExist is null || categoryExist.NgayXoa.HasValue)
            {
                return "Category is not found or deleted";
            }

            string imageUrl = string.Empty;
            if (request.ImageFile != null)
            {
                using (var stream = request.ImageFile.OpenReadStream())
                {
                    imageUrl = await _fileUploadService.UploadFileAsync(stream, $"{Guid.NewGuid()}.jpg");
                }
            }

            categoryExist.Name = request.Name ?? categoryExist.Name;
            categoryExist.Url = !string.IsNullOrEmpty(imageUrl) ? imageUrl : categoryExist.Url;

            categoryExist.NguoiCapNhatID = _currentUserService.UserId;
            categoryExist.NgayCapNhat = DateTime.Now;
            _categoryRepository.Update(categoryExist);

            return await _categoryRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Update Success!" : "Update Fail!";
        }
    }
}
