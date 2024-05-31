using AutoMapper;
using FluentValidation;
using MediatR;
using OrderingKioskSystem.Application.Common.Pagination;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Order.GetByPagnition
{
    public class GetOrderByPagnitionQueryHandler : IRequestHandler<GetOrderByPagnitionQuery, PagedResult<OrderDTO>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public GetOrderByPagnitionQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<OrderDTO>> Handle(GetOrderByPagnitionQuery query, CancellationToken cancellationToken)
        {
            var list = await _orderRepository.FindAllAsync(
                x => x.NgayXoa == null,
                query.PageNumber,
                query.PageSize,
                queryOptions: q => q
                .OrderBy(x => x.Status == "OnPreparing" ? 1 :
                      x.Status == "Prepared" ? 2 :
                      x.Status == "OnDelivery" ? 3 :
                      x.Status == "Deliveried" ? 4 : 5)
                .ThenByDescending(x => x.NgayTao),
                cancellationToken
            );
            return PagedResult<OrderDTO>.Create
            (
                totalCount: list.TotalCount,
                pageCount: list.PageCount,
                pageSize: list.PageSize,
                pageNumber: list.PageNo,
                data: list.MapToOrderDTOList(_mapper)
                );
        }
    }
}
