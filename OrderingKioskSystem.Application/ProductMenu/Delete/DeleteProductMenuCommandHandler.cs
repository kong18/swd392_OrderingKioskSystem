using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Application.ProductMenu.Update;
using OrderingKioskSystem.Domain.Repositories;
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
        private readonly ICurrentUserService _currentUserService;

        public DeleteProductMenuCommandHandler(IProductMenuRepository productMenuRepository, ICurrentUserService currentUserService)
        {
            _productMenuRepository = productMenuRepository;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(DeleteProductMenuCommand request, CancellationToken cancellationToken)
        {
            var productMenuExist = await _productMenuRepository.FindAsync(x => x.ID == request.ID && !x.NgayXoa.HasValue, cancellationToken);
            if (productMenuExist == null)
            {
                return "Product is not found or deleted";
            }

            productMenuExist.NgayXoa = DateTime.Now;
            productMenuExist.NguoiXoaID = _currentUserService.UserId;

            _productMenuRepository.Update(productMenuExist);

            return await _productMenuRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Delete Success!" : "Delete Fail!";
        }
    }
}
