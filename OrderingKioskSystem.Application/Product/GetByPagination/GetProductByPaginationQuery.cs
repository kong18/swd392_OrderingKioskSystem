using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Application.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Product.GetByPagination
{
    public class GetProductByPaginationQuery : IRequest<PagedResult<ProductDTO>>, IQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public GetProductByPaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        public GetProductByPaginationQuery() { }
    }
}
