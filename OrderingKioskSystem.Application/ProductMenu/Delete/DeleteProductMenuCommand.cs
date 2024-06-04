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
        public DeleteProductMenuCommand(string id)
        { 
            ID = id;
        }
        public string ID { get; set; }
    }
}
