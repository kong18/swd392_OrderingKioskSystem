using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using OrderingKioskSystem.Domain.Common.Exceptions;
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
                throw new NotFoundException("Menu is not found or deleted");
            }

            foreach (var item in request.Products)
            {
                bool productExist = await _productRepository.AnyAsync(x => x.ID == item.ProductID && !x.NgayXoa.HasValue, cancellationToken);

                if (!productExist)
                {
                    throw new NotFoundException($"Product with ID {item.ProductID} is not found or deleted");
                }
            }

            foreach (var item in request.Products)
            {
                var productMenu = new ProductMenuEntity
                {
                    ProductID = item.ProductID,
                    MenuID = menuExist.ID,
                    Price = item.Price,
                };

                _productMenuRepository.Add(productMenu);
                await _productMenuRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }

            return "Add Success";
        }
    }
}
