using MediatR;
using OrderingKioskSystem.Application.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Menu.Filter
{
    public class FilterMenuQuery : IRequest<PagedResult<MenuDTO>>
    {
        public string? Title { get; set; }
        public string? Name { get; set; }
        public bool? Status { get; set; }
        public string? Type { get; set; }
        public string? BusinessID { get; set; }
        public string? BusinessName { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
