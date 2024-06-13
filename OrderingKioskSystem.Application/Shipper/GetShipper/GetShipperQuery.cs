using MediatR;
using OrderingKioskSystem.Application.Business;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Application.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Shipper.GetAllShipper
{
    public class GetShipperQuery : IRequest<PagedResult<ShipperDTO>>, IQuery
    {
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public bool? SortOrder { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
