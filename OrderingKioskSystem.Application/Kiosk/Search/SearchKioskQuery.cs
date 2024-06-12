using MediatR;
using OrderingKioskSystem.Application.Common.Pagination;
using System.Collections.Generic;

namespace OrderingKioskSystem.Application.Kiosk.Get
{
    public class SearchKioskQuery : IRequest<PagedResult<KioskDTO>>
    {
        public string? Location { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
