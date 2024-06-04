using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Application.ProductMenu.Create;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.ProductMenu.Update
{
    public class UpdateProductMenuCommandHandler : IRequestHandler<UpdateProductMenuCommand, string>
    {
        private readonly IProductMenuRepository _productMenuRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateProductMenuCommandHandler(IProductMenuRepository productMenuRepository, IMenuRepository menuRepository, IProductRepository productRepository, ICurrentUserService currentUserService)
        {
            _productMenuRepository = productMenuRepository;
            _menuRepository = menuRepository;
            _productRepository = productRepository;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(UpdateProductMenuCommand request, CancellationToken cancellationToken)
        {
            var productMenuExist = await _productMenuRepository.FindAsync(x => x.ID == request.ID && !x.NgayXoa.HasValue, cancellationToken);
            if (productMenuExist == null)
            {
                return "Product is not found or deleted";
            }

            if(!string.IsNullOrEmpty(request.MenuID))
            {
                var menuExist = await _menuRepository.FindAsync(x => x.ID == request.MenuID && !x.NgayXoa.HasValue, cancellationToken);
                if (menuExist is null)
                {
                    return "Menu is not found or deleted";
                }
            }

            if (!string.IsNullOrEmpty(request.ProductID))
            {
                var productExist = await _productRepository.FindAsync(x => x.ID == request.ProductID && !x.NgayXoa.HasValue, cancellationToken);
                if (productExist is null)
                {
                    return "Product is not found or deleted";
                }
            }

            productMenuExist.ProductID = request.ProductID ?? productMenuExist.ProductID;
            productMenuExist.MenuID = request.ProductID ?? productMenuExist.ProductID;
            productMenuExist.Price = request.Price ?? productMenuExist.Price;

            productMenuExist.NgayCapNhatCuoi = DateTime.Now;
            productMenuExist.NguoiCapNhatID = _currentUserService.UserId;

            _productMenuRepository.Update(productMenuExist);

            return await _productMenuRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Update Success!" : "Update Fail!";
        }
    }
}
