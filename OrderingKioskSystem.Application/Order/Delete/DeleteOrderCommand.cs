using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Order.Delete
{
    public class DeleteOrderCommand : IRequest<string>
    {
        public DeleteOrderCommand(string id) 
        {
            ID = id;
        }
        public string ID { get; set; }
    }
}
