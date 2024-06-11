﻿using MediatR;
using Microsoft.EntityFrameworkCore.Query.Internal;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Application.FileUpload;
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
        private readonly ICurrentUserService _currentUserService;
        private readonly FileUploadService _fileUploadService;

        public UpdateProductCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IBusinessRepository businessRepository, ICurrentUserService currentUserService, FileUploadService fileUploadService)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _businessRepository = businessRepository;
            _currentUserService = currentUserService;
            _fileUploadService = fileUploadService;
        }

        public async Task<string> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productExist = await _productRepository.FindAsync(x => x.ID == request.ID, cancellationToken);

            if (productExist is null || productExist.NgayXoa.HasValue)
            {
                return "ProductID does not exist";
            }

            if (request.CategoryID != null)
            {
                bool categoryExist = await _categoryRepository.AnyAsync(x => x.ID == request.CategoryID, cancellationToken);

                if (!categoryExist)
                {
                    return "CategoryID does not exist";
                }
            }

            var businessID = _currentUserService.UserId;
            
            bool businessExist = await _businessRepository.AnyAsync(x => x.ID == businessID, cancellationToken);

            if (!businessExist)
            {
                return "BusinessID does not exist";
            }

            string imageUrl = string.Empty;
            if (request.ImageFile != null)
            {
                using (var stream = request.ImageFile.OpenReadStream())
                {
                    imageUrl = await _fileUploadService.UploadFileAsync(stream, $"{Guid.NewGuid()}.jpg");
                }
            }

            productExist.Name = request.Name ?? productExist.Name;
            productExist.Code = request.Code ?? productExist.Code;
            productExist.Url = !string.IsNullOrEmpty(imageUrl) ? imageUrl : productExist.Url;
            productExist.Description = request.Description ?? productExist.Description;
            productExist.Price = request.Price ?? productExist.Price;
            productExist.Status = request.Status ?? productExist.Status;
            productExist.CategoryID = request.CategoryID ?? productExist.CategoryID;
            
            productExist.NguoiCapNhatID = businessID;
            productExist.NgayCapNhatCuoi = DateTime.Now;
            _productRepository.Update(productExist);

            return await _productRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Update Success!" : "Update Fail!";
        }
    }
}
