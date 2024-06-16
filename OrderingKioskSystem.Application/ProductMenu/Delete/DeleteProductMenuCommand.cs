using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.ProductMenu.Delete
{
    public class DeleteProductMenuCommand : IRequest<string>
    {
        public DeleteProductMenuCommand(string menuID, List<RequestItem> products)
        { 
            MenuID = menuID;
            Products = products;
        }
        public string MenuID { get; set; }
        public List<RequestItem> Products { get; set; }
    }

    public class RequestItem
    {
        public string ProductID { get; set; }
    }
}
