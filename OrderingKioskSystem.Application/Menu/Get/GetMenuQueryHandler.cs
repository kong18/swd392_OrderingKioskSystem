using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Menu.GetById;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Menu.GetAll
{
    public class GetMenuQueryHandler : IRequestHandler<GetMenuQuery, List<MenuDTO>>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;
        public GetMenuQueryHandler(IMenuRepository menuRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<List<MenuDTO>> Handle(GetMenuQuery request, CancellationToken cancellationToken)
        {
            var menuList = await _menuRepository.FindAllAsync(x => !x.NgayXoa.HasValue, cancellationToken);

            if (menuList is null)
            {
                throw new NotFoundException("Menu's List is empty");
            }

            return menuList.MapToMenuDTOList(_mapper);
        }
    }
}
