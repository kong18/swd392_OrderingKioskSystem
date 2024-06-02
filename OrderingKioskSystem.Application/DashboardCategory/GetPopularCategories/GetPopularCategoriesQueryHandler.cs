using MediatR;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.DashboardCategory.GetPopularCategories
{
    public class GetPopularCategoriesQueryHandler : IRequestHandler<GetPopularCategoriesQuery, List<PopularCategoryDTO>>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductRepository _productRepository;

        public GetPopularCategoriesQueryHandler(IOrderDetailRepository orderDetailRepository, IProductRepository productRepository)
        {
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
        }

        public async Task<List<PopularCategoryDTO>> Handle(GetPopularCategoriesQuery request, CancellationToken cancellationToken)
        {
            var orderDetails = await _orderDetailRepository.FindAllAsync(cancellationToken);
            var products = await _productRepository.FindAllAsync(cancellationToken);

            var popularCategories = orderDetails
                .GroupBy(od => products.FirstOrDefault(p => p.ID == od.ProductID)?.CategoryID)
                .Select(g => new PopularCategoryDTO
                {
                    CategoryName = products.FirstOrDefault(p => p.CategoryID == g.Key)?.Category.Name,
                    TotalSales = g.Count(),
                    TotalAmount = g.Sum(od => od.Price)
                })
                .OrderByDescending(pc => pc.TotalSales)
                .ToList();

            return popularCategories;
        }
    }
}