using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.ProductMenu.Create
{
    public class CreateProductMenuCommand : IRequest<string>
    {
        public CreateProductMenuCommand(string menuID, List<RequestItem> products)
        {
            MenuID = menuID;
            Products = products;
        }
        public string MenuID {  get; set; }
        public List<RequestItem> Products { get; set; }
    }

    public class RequestItem
    {
        public string ProductID { get; set; }
        public decimal Price { get; set; }
    }
}
