using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Common.Pagination;
using OrderingKioskSystem.Application.Order.GetByPagnition;
using OrderingKioskSystem.Application.Order;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingKioskSystem.Application.Product;
using OrderingKioskSystem.Infrastructure.Repositories;

namespace OrderingKioskSystem.Application.Menu.GetByPagnition
{
    public class GetMenuByPagnitionQueryHandler : IRequestHandler<GetMenuByPagnitionQuery, PagedResult<MenuDTO>>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;
        public GetMenuByPagnitionQueryHandler(IMenuRepository menuRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<MenuDTO>> Handle(GetMenuByPagnitionQuery query, CancellationToken cancellationToken)
        {
            var list = await _menuRepository.FindAllAsync(x => x.NgayXoa == null, query.PageNumber, query.PageSize, queryOptions: q => q.OrderBy(x => x.NgayTao), cancellationToken);
            return PagedResult<MenuDTO>.Create
            (
                totalCount: list.TotalCount,
                pageCount: list.PageCount,
                pageSize: list.PageSize,
                pageNumber: list.PageNo,
                data: list.MapToMenuDTOList(_mapper)
                );
        }
    }
}
