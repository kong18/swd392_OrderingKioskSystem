using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Order.GetAll
{
    public class GetOrderQuery : IRequest<List<OrderDTO>>
    {
        public GetOrderQuery() { }
    }
}
