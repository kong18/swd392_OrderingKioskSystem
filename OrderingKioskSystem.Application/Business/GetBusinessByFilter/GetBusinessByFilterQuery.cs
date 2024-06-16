using MediatR;
using OrderingKioskSystem.Application.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Business.GetBusinessByFilter
{
    public class GetBusinessByFilterQuery : IRequest<PagedResult<BusinessDTO>>
    {
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? BankName { get; set; }
        public bool? SortOrder { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
