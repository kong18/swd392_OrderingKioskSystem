using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingKioskSystem.Infrastructure.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.Dashboard
{
    public class GetSalesDataQueryHandler : IRequestHandler<GetSalesDataQuery, SalesDataDTO>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSalesDataQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SalesDataDTO> Handle(GetSalesDataQuery request, CancellationToken cancellationToken)
        {
            // Fetching menu sales data
            var menuSales = await _context.Menus
                .Select(m => new
                {
                    m.Name,
                    TotalSales = m.ProductMenus
                        .SelectMany(pm => pm.Product.OrderDetails)
                        .Select(od => od.Quantity * od.UnitPrice)
                        .Sum()
                }).ToListAsync(cancellationToken);

            var dailySales = menuSales.Select(ms => new MenuSalesDTO
            {
                Label = ms.Name,
                Value = ms.TotalSales
            }).ToList();

            // Fetching popular category sales
            var popularCategorySales = await _context.Products
                .GroupBy(p => p.Category.Name)
                .Select(g => new PopularCategorySalesDTO
                {
                    Category = g.Key,
                    TotalSales = g.SelectMany(p => p.OrderDetails)
                                  .Select(od => od.Quantity * od.UnitPrice)
                                  .Sum()
                }).ToListAsync(cancellationToken);

            // Fetching product sales data
            var productSales = await _context.Products
                .Select(p => new
                {
                    p.Name,
                    TotalSales = p.OrderDetails.Sum(od => od.Quantity * od.UnitPrice),
                    TotalOrders = p.OrderDetails.Sum(od => od.Quantity)
                }).ToListAsync(cancellationToken);

            var products = productSales.Select(ps => new ProductSalesDTO
            {
                Name = ps.Name,
                TotalSales = ps.TotalSales,
                TotalOrders = ps.TotalOrders
            }).ToList();

            return new SalesDataDTO
            {
                DailySales = dailySales,
                PopularCategorySales = popularCategorySales,
                ProductSales = products
            };
        }
    }
}
