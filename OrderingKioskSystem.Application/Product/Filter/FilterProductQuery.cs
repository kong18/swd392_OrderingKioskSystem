    using MediatR;
    using OrderingKioskSystem.Application.Common.Pagination;

    namespace OrderingKioskSystem.Application.Product.Filter
    {
        public class FilterProductQuery : IRequest<PagedResult<ProductDTO>>
        {
            public string ?name { get; set; }
            public string ?code { get; set; }
            public bool? sortorder { get; set; }
            public bool? status { get; set; }
            public int? categoryid { get; set; }
            public string ? businessid { get; set; }
            public int pagenumber { get; set; } = 1;
            public int pagesize { get; set; } = 10;
        }
    }
