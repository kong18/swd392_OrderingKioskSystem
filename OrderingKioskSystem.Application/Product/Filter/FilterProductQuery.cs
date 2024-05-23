using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Application.Common.Pagination;

namespace OrderingKioskSystem.Application.Product.Filter
{
    public class FilterProductQuery : IRequest<PagedResult<ProductDTO>>, IQuery
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? IsAvailable { get; set; }
        public bool? Status { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
