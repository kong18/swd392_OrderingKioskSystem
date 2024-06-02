using MediatR;
using OrderingKioskSystem.Application.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Menu.GetByPagnition
{
    public class GetMenuByPagnitionQuery : IRequest<PagedResult<MenuDTO>>
    {
        public GetMenuByPagnitionQuery() { }
        public GetMenuByPagnitionQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber {  get; set; }
        public int PageSize { get; set; }

    }
}
