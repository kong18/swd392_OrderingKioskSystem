using MediatR;
using OrderingKioskSystem.Application.Common.Pagination;
using OrderingKioskSystem.Application.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Order.GetByPagnition
{
    public class GetOrderByPagnitionQuery : IRequest<PagedResult<OrderDTO>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetOrderByPagnitionQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        public GetOrderByPagnitionQuery()
        {
        }
    }
}
