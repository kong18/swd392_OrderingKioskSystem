using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Product.GetById;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Product.GetAll
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, List<ProductDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDTO>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.FindAllAsync(x => !x.NgayXoa.HasValue,cancellationToken);

            if (products.Count == 0)
            {
                throw new NotFoundException("Product List is empty");
            }
            return products.MapToProductDTOList(_mapper);
        }
    }
}
