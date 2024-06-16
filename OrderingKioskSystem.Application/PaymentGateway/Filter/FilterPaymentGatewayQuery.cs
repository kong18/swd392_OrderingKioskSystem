using MediatR;
using OrderingKioskSystem.Application.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.PaymentGateway.Filter
{
    public class FilterPaymentGatewayQuery : IRequest<PagedResult<PaymentGatewayDTO>>
    {
        public string? Name { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
