using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingKioskSystem.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var menus = await _context.Menus
                .Include(m => m.ProductMenus)
                    .ThenInclude(pm => pm.Product)
                        .ThenInclude(p => p.OrderDetails)
                .Select(m => new MenuSalesDTO
                {
                    Label = m.Name,
                    Value = m.ProductMenus.Sum(pm => pm.Product.OrderDetails.Sum(od => od.Quantity * od.Price))
                }).ToListAsync(cancellationToken);

            // Fetching products and calculating total amount sold
            var products = await _context.Products
                .Include(p => p.OrderDetails)
                .Select(p => new ProductSalesDTO
                {
                    Label = p.Name,
                    Value = p.OrderDetails.Sum(od => od.Quantity)
                }).ToListAsync(cancellationToken);

            return new SalesDataDTO
            {
                Menus = menus,
                Products = products
            };
        }
    }
}
