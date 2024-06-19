using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.Dashboard
{
    public class SalesDataDTO
    {
        public List<MenuSalesDTO> DailySales { get; set; }
        public List<PopularCategorySalesDTO> PopularCategorySales { get; set; }
        public List<ProductSalesDTO> ProductSales { get; set; }
    }
}
