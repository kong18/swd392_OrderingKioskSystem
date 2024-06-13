using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.Dashboard
{
    public class SalesDataDTO
    {
        public List<MenuSalesDTO> Menus { get; set; }
        public List<ProductSalesDTO> Products { get; set; }
    }
}
