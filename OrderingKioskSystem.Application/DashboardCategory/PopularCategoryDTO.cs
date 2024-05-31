using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.DashboardCategory
{
    public class PopularCategoryDTO
    {
        public string CategoryName { get; set; }
        public int TotalSales { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
