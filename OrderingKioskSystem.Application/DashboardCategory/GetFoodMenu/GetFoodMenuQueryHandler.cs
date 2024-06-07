using MediatR;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.DashboardCategory.GetFoodMenu
{
    public class GetFoodMenuQueryHandler : IRequestHandler<GetFoodMenuQuery, List<FoodMenuDTO>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public GetFoodMenuQueryHandler(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<List<FoodMenuDTO>> Handle(GetFoodMenuQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.FindAllAsync(cancellationToken);
            var orderDetails = orders.SelectMany(order => order.OrderDetails).ToList();

            var foodMenuSales = orderDetails
                .GroupBy(detail => detail.Product.Name)
                .Select(group => new FoodMenuDTO
                {
                    ProductName = group.Key,
                    TotalSale = group.Sum(detail => detail.Price),
                    TotalOrder = group.Count()
                }).ToList();

            return foodMenuSales;
        }
    }
}
