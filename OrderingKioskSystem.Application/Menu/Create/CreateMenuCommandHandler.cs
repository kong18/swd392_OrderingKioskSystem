using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using OrderingKioskSystem.Domain.Common.Exceptions;
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
            var businessID = _currentUserService.UserId;

            bool businessExist = await _businessRepository.AnyAsync(x => x.ID == businessID && !x.NgayXoa.HasValue, cancellationToken);

            if (!businessExist)
            {
                throw new NotFoundException("Business does not exist");
            }

            var existingMenus = await _menuRepository.FindAllAsync(x => x.BusinessID == businessID && x.Type == request.Type && !x.NgayXoa.HasValue, cancellationToken);

            if (existingMenus.Any())
            {
                return $"Business already has a {request.Type} menu";
            }

            foreach (var item in request.Products)
            {
                bool productExist = await _productRepository.AnyAsync(x => x.ID == item.ProductID && !x.NgayXoa.HasValue, cancellationToken);

                if (!productExist)
                {
                    throw new NotFoundException($"Product with ID {item.ProductID} is not found or deleted");
                }
            }

            // Create and add the new menu
            var menu = new MenuEntity
            {
                Name = request.Name,
                Title = request.Title,
                Type = request.Type,
                Status = request.Status,
                BusinessID = businessID ?? "",
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
                await _productMenuRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }

            return "Create Success";

        }
    }
}
