using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Menu.Create
{
    public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, string>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductMenuRepository _productMenuRepository;
        private readonly IBusinessRepository _businessRepository;
        private readonly ICurrentUserService _currentUserService;
        public CreateMenuCommandHandler(IMenuRepository menuRepository, IProductMenuRepository productMenuRepository, IProductRepository productRepository, ICurrentUserService currentUserService, IBusinessRepository businessRepository)
        {
            _menuRepository = menuRepository;
            _productRepository = productRepository;
            _currentUserService = currentUserService;
            _productMenuRepository = productMenuRepository;
            _businessRepository = businessRepository;
        }

        public async Task<string> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            bool businessExist = await _businessRepository.AnyAsync(x => x.ID == request.BusinessID && !x.NgayXoa.HasValue, cancellationToken);

            if (!businessExist)
            {
                return "Business does not exist";
            }

            foreach (var item in request.Products)
            {
                bool productExist = await _productRepository.AnyAsync(x => x.ID == item.ProductID && !x.NgayXoa.HasValue, cancellationToken);

                if (!productExist)
                {
                    return $"Product with ID {item.ProductID} is not found or deleted";
                }
            }

            // Get the DbContext from the UnitOfWork
            var dbContext = _menuRepository.UnitOfWork as DbContext;
            if (dbContext == null)
            {
                throw new InvalidOperationException("The UnitOfWork is not associated with a DbContext.");
            }

            using (var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    // Create and add the new menu
                    var menu = new MenuEntity
                    {
                        Name = request.Name,
                        Title = request.Title,
                        Type = request.Type,
                        Status = request.Status,
                        BusinessID = request.BusinessID,
                        NguoiTaoID = _currentUserService.UserId,
                        NgayTao = DateTime.Now
                    };

                    _menuRepository.Add(menu);
                    await _menuRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                    var menuID = menu.ID;

                    // Add each product to the menu
                    foreach (var item in request.Products)
                    {
                        var productMenu = new ProductMenuEntity
                        {
                            ProductID = item.ProductID,
                            Price = item.Price,
                            MenuID = menuID,
                            NgayTao = DateTime.Now,
                            NguoiTaoID = _currentUserService.UserId,
                        };

                        _productMenuRepository.Add(productMenu);
                    }

                    // Save changes for all product menus in a single transaction
                    var saveResult = await _productMenuRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

                    if (saveResult > 0)
                    {
                        await transaction.CommitAsync(cancellationToken);
                        return "Create Success!";
                    }
                    else
                    {
                        await transaction.RollbackAsync(cancellationToken);
                        return "Create Failed!";
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    // Log the exception (ex)
                    return $"An error occurred: {ex.Message}";
                }
            }
        }
    }
}
