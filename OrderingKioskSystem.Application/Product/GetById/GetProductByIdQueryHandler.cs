using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using OrderingKioskSystem.Application.Product.Create;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Product.GetById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDTO>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper, IMemoryCache cache)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<ProductDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            // Check if the product is in the cache
            if (_cache.TryGetValue(request.ID, out ProductEntity cachedProduct) && cachedProduct.NgayXoa == null)
            {
                return _mapper.Map<ProductDTO>(cachedProduct);
            }

            // Retrieve product from the database
            var product = await _productRepository.FindAsync(x => x.ID == request.ID, cancellationToken);

            if (product == null || product.NgayXoa.HasValue)
            {
                throw new NotFoundException("Product is not found or deleted");
            }

            // Set the product in the cache
            _cache.Set(product.ID, product, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                SlidingExpiration = TimeSpan.FromMinutes(2)
            });

            return product.MapToProductDTO(_mapper);
        }
    }
}
