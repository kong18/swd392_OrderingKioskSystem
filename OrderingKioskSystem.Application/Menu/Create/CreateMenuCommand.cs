using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Menu.Create
{
    public class CreateMenuCommand : IRequest<string>
    {
        public CreateMenuCommand(string name, string title, string type, bool status, string businessID, List<RequestItem> products)
        {
            Name = name;
            Title = title;
            Type = type;
            Status = status;
            BusinessID = businessID;
            Products = products;
        }

        public string Name { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; } = true;
        public string BusinessID { get; set; }
        public List<RequestItem> Products { get; set; }
    }

    public class RequestItem
    {
        public string ProductID { get; set; }
        public decimal Price {  get; set; }
    }
}
