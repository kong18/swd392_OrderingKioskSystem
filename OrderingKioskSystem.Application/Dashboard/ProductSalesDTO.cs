﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.Dashboard
{
    public class ProductSalesDTO
    {
        public string Name { get; set; }
        public decimal TotalSales { get; set; }
        public int TotalOrders { get; set; }
    }
}
