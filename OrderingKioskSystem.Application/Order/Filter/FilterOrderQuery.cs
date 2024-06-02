using MediatR;
using OrderingKioskSystem.Application.Common.Pagination;
using OrderingKioskSystem.Application.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Order.Filter
{
    public class FilterOrderQuery : IRequest<PagedResult<OrderDTO>>
    {
        public string? KioskID { get; set; }
        public string? Location { get; set; }
        public decimal? MinTotal { get; set; }
        public decimal? MaxTotal { get; set; }
        public string? Note { get; set; }
        public string? Status { get; set; }
        public string? ShipperID { get; set; }
        public string? ShipperName { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
