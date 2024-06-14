using MediatR;
using OrderingKioskSystem.Domain.Repositories;
using OrderingKioskSystem.Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.ProductMenu.Delete
{
    public class DeleteProductMenuCommandHandler : IRequestHandler<DeleteProductMenuCommand, string>
    {
        private readonly IProductMenuRepository _productMenuRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IProductRepository _productRepository;

        public DeleteProductMenuCommandHandler(IProductMenuRepository productMenuRepository, IMenuRepository menuRepository, IProductRepository productRepository)
        {
            _productMenuRepository = productMenuRepository;
            _menuRepository = menuRepository;
            _productRepository = productRepository;
        }

        public async Task<string> Handle(DeleteProductMenuCommand request, CancellationToken cancellationToken)
        {
            var menuExist = await _menuRepository.FindAsync(x => x.ID == request.MenuID && !x.NgayXoa.HasValue, cancellationToken);
            if (menuExist is null)
            {
                throw new NotFoundException( "Menu is not found or deleted");
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
                var productMenuExist = await _productMenuRepository.FindAsync(x => x.MenuID == request.MenuID && x.ProductID == item.ProductID, cancellationToken);

                if (productMenuExist is null)
                {
                    throw new NotFoundException("Product in Menu not found");
                }

                _productMenuRepository.Remove(productMenuExist);
            }
            return await _productMenuRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Delete Success!" : "Delete Fail!";
        }
    }
}
