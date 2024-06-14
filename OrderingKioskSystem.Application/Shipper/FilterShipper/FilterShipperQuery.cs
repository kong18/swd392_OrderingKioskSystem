using MediatR;
using OrderingKioskSystem.Application.Common.Pagination;
using OrderingKioskSystem.Application.Shipper;

namespace SWD.OrderingKioskSystem.Application.Shipper.FilterShipper
{
    public class FilterShipperQuery : IRequest<PagedResult<ShipperDTO>>
    {
        public string? Name { get; set; }
        public string? Id { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
