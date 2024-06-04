using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.ProductMenu.Create
{
    public class CreateProductMenuCommandHandler : IRequestHandler<CreateProductMenuCommand, string>
    {
        private readonly IProductMenuRepository _productMenuRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IProductRepository _productRepository;

        public CreateProductMenuCommandHandler(IProductMenuRepository productMenuRepository, IMenuRepository menuRepository, IProductRepository productRepository)
        {
            _productMenuRepository = productMenuRepository;
            _menuRepository = menuRepository;
            _productRepository = productRepository;
        }

        public async Task<string> Handle(CreateProductMenuCommand request, CancellationToken cancellationToken)
        {
            var menuExist = await _menuRepository.FindAsync(x => x.ID == request.MenuID && !x.NgayXoa.HasValue, cancellationToken);
            if (menuExist is null)
            {
                return "Menu is not found or deleted";
            }

            var dbContext = _productMenuRepository.UnitOfWork as DbContext;
            if (dbContext == null)
            {
                throw new InvalidOperationException("The UnitOfWork is not associated with a DbContext.");
            }

            using (var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    foreach (var item in request.Products)
                    {
                        var product = await _productRepository.FindAsync(x => x.ID == item.ProductID && !x.NgayXoa.HasValue, cancellationToken);

                        if (product == null)
                        {
                            return $"Product with {item.ProductID} is not found or deleted";
                        }

                        var productMenu = new ProductMenuEntity
                        {
                            ProductID = item.ProductID,
                            MenuID = menuExist.ID,
                            Price = item.Price,
                        };

                        _productMenuRepository.Add(productMenu);
                    }

                    var saveResult = await _productMenuRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

                    if (saveResult > 0)
                    {
                        await transaction.CommitAsync(cancellationToken);
                        return "Add Success";
                    }
                    else
                    {
                        await transaction.RollbackAsync(cancellationToken);
                        return "Add Fail";
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
