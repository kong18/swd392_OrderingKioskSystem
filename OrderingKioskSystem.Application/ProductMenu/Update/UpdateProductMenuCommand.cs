using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.ProductMenu.Update
{
    public class UpdateProductMenuCommand : IRequest<string>
    {
        public UpdateProductMenuCommand(string iD, string? menuID, string? productID, decimal price)
        {
            ID = iD;
            MenuID = menuID;
            ProductID = productID;
            Price = price;
        }

        public string ID { get; set; }
        public string? MenuID { get; set; }
        public string? ProductID { get; set; }
        public decimal? Price { get; set; }
    }
}
