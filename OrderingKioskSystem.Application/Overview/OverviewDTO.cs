using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Overview
{
    public class OverviewDTO
    {
        public string TimePeriod { get; set; }
        public decimal TotalSales { get; set; }
        public int TotalOrders { get; set; }
    }

}
