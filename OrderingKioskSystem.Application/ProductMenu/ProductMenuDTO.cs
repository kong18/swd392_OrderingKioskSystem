using OrderingKioskSystem.Application.Menu;
using OrderingKioskSystem.Application.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.ProductMenu
{
    public class ProductMenuDTO
    {
        public string ID { get; set; }
        public string MenuID { get; set; }
        public string ProductID { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public MenuDTO Menu { get; set; }
        public ProductDTO Product { get; set; }
    }
}
