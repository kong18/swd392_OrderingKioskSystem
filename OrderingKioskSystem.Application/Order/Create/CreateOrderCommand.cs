using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Order.Create
{
    public class CreateOrderCommand : IRequest<OrderDTO>, ICommand
    {
        public CreateOrderCommand(string kioskID, List<RequestItem> products, string Note)
        {
            KioskID = kioskID;
            Products = products;
            this.Note = Note;
        }
        
        public string KioskID { get; set; }
        public string? Note {  get; set; }
        public List<RequestItem> Products { get; set; }
    }

    public class RequestItem
    {
        public string ProductID { get; set; }
        public int Quantity { get; set; }
        public string? Size { get; set; }
    }

}
