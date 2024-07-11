using MediatR;
using Microsoft.Extensions.Caching.Memory;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Product.Delete
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, string>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMemoryCache _cache;
        private readonly ICurrentUserService _currentUserService;

        public DeleteProductCommandHandler(IProductRepository productRepository, IMemoryCache cache, ICurrentUserService currentUserService)
        {
            _productRepository = productRepository;
            _cache = cache;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindAsync(x => x.ID == request.id, cancellationToken);

            if (product == null)
            {
                return "Product not found.";
            }

            product.NgayXoa = DateTime.UtcNow;
            product.NguoiXoaID = _currentUserService.UserId;

            _productRepository.Update(product);

            if (await _productRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0)
            {
                // Invalidate the cache
                _cache.Remove(product.ID);

                return "Product deleted successfully.";
            }
            else
            {
                return "Product deletion failed.";
            }
        }
    }
}
