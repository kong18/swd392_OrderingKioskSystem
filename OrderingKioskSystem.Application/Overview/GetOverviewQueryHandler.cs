using MediatR;
using OrderingKioskSystem.Domain.Entities;
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
            var overviewData = new List<OverviewDTO>();

            // Group by Day
            var dailyOrders = orders.GroupBy(order => order.NgayTao.Value.Date);
            overviewData.AddRange(ProcessOrders(dailyOrders, "Day"));

            // Group by Week
            var weeklyOrders = orders.GroupBy(order => GetWeekOfYear(order.NgayTao.Value));
            overviewData.AddRange(ProcessOrders(weeklyOrders, "Week"));

            // Group by Month
            var monthlyOrders = orders.GroupBy(order => new { order.NgayTao.Value.Year, order.NgayTao.Value.Month });
            overviewData.AddRange(ProcessOrders(monthlyOrders, "Month"));

            return overviewData;
        }

        private List<OverviewDTO> ProcessOrders<TKey>(IEnumerable<IGrouping<TKey, OrderEntity>> groupedOrders, string periodType)
        {
            var overviewData = new List<OverviewDTO>();

            foreach (var group in groupedOrders)
            {
                var timePeriods = group
                    .GroupBy(order => GetTimePeriod(order.NgayTao.Value))
                    .Select(timeGroup => new
                    {
                        TimePeriod = timeGroup.Key,
                        TotalSales = timeGroup.Sum(order => order.Total),
                        TotalOrders = timeGroup.Count()
                    }).ToList();

                foreach (var timePeriod in timePeriods)
                {
                    overviewData.Add(new OverviewDTO
                    {
                        TimePeriod = $"{group.Key} - {timePeriod.TimePeriod}",
                        TotalSales = timePeriod.TotalSales,
                        TotalOrders = timePeriod.TotalOrders,
                        PeriodType = periodType
                    });
                }
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

        private (int Year, int Week) GetWeekOfYear(DateTime date)
        {
            var d = date;
            var cal = System.Globalization.CultureInfo.CurrentCulture.Calendar;
            var week = cal.GetWeekOfYear(d, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return (d.Year, week);
        }
    }

}
