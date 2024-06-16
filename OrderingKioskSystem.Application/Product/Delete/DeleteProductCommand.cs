using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Product.Delete
{
    public class DeleteProductCommand : IRequest<string>
    {
        public DeleteProductCommand(string Id)
        {
            id = Id;
        }

        public string id { get; set; }
    }
}
