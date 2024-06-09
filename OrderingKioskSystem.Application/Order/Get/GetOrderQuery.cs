using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Order.GetAll
{
    public class GetAllOrderQuery : IRequest<List<OrderDTO>>
    {
        public GetAllOrderQuery() { }
    }
}
