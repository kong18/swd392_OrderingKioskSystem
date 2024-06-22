using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Order.Create
{
    public class CreateOrderCommand : IRequest<OrderDTO>, ICommand
    {
        public CreateOrderCommand(List<RequestItem> products, string Note)
        {
            Products = products;
            this.Note = Note;
        }
        
        public string? Note {  get; set; }
        public decimal Total { get; set; } = 0;
        public List<RequestItem> Products { get; set; }
    }

    public class RequestItem
    {
        public string ProductID { get; set; }
        public int Quantity { get; set; }
        public string? Size { get; set; }
    }

}
