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

namespace OrderingKioskSystem.Application.Product.Delete
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, string>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<string> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindAsync(x => x.ID == request.id, cancellationToken);

            if (product is null || product.NgayXoa.HasValue)
            {
                return "Product is not found or deleted";
            }

            product.NgayXoa = DateTime.Now;
            _productRepository.Update(product);

            return await _productRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Delete Success!" : "Delete Fail!";
        }
    }
}
