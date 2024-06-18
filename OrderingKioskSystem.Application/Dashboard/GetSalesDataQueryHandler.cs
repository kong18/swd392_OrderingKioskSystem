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
            // Fetching menus and calculating total sales
            var menuSales = await _context.Menus
                .Select(m => new
                {
                    m.Name,
                    TotalSales = m.ProductMenus
                        .SelectMany(pm => pm.Product.OrderDetails)
                        .Sum(od => od.Quantity * od.Price)
                }).ToListAsync(cancellationToken);

            var menus = menuSales.Select(ms => new MenuSalesDTO
            {
                Label = ms.Name,
                Value = ms.TotalSales
            }).ToList();

            // Fetching products and calculating total amount sold
            var productSales = await _context.Products
                .Select(p => new
                {
                    p.Name,
                    TotalAmountSold = p.OrderDetails.Sum(od => od.Quantity)
                }).ToListAsync(cancellationToken);

            var products = productSales.Select(ps => new ProductSalesDTO
            {
                Label = ps.Name,
                Value = ps.TotalAmountSold
            }).ToList();

            return new SalesDataDTO
            {
                Menus = menus,
                Products = products
            };
        }
    }
}
