using MediatR;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Overview
{
    public class GetOverviewQueryHandler : IRequestHandler<GetOverviewQuery, List<OverviewDTO>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOverviewQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OverviewDTO>> Handle(GetOverviewQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.FindAllAsync(cancellationToken);
            var groupedOrders = orders
                .Where(order => order.NgayTao.HasValue)
                .GroupBy(order => GetTimePeriod(order.NgayTao.Value));

            var overviewData = new List<OverviewDTO>();
            foreach (var group in groupedOrders)
            {
                var totalSales = group.Sum(order => order.Total);
                var totalOrders = group.Count();
                overviewData.Add(new OverviewDTO
                {
                    TimePeriod = group.Key,
                    TotalSales = totalSales,
                    TotalOrders = totalOrders
                });
            }

            return overviewData;
        }

        private string GetTimePeriod(DateTime orderDate)
        {
            if (orderDate.Hour >= 6 && orderDate.Hour < 12)
            {
                return "Sáng";
            }
            if (orderDate.Hour >= 12 && orderDate.Hour < 18)
            {
                return "Trưa";
            }
            return "Chiều";
        }
    }
}
