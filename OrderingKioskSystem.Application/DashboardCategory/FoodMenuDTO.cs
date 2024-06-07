using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.DashboardCategory
{
    public class FoodMenuDTO
    {
        public string ProductName { get; set; }
        public decimal TotalSale { get; set; }
        public int TotalOrder { get; set; }
    }
}
