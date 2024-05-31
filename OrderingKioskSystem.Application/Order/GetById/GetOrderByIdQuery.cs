using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Order.GetById
{
    public class GetOrderByIdQuery : IRequest<OrderDTO>
    {
        public GetOrderByIdQuery(string id)
        {
            ID = id;
        }
        public string ID { get; set; }
    }
}
