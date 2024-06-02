using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Application.Menu.Delete;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingKioskSystem.Domain.Common.Exceptions;

namespace OrderingKioskSystem.Application.Menu.GetById
{
    public class GetMenuByIdQueryHandler : IRequestHandler<GetMenuByIdQuery, MenuDTO>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;
        public GetMenuByIdQueryHandler(IMenuRepository menuRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<MenuDTO> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
        {
            var menuExist = await _menuRepository.FindAsync(x => x.ID == request.ID && !x.NgayXoa.HasValue, cancellationToken);

            if (menuExist == null)
            {
                throw new NotFoundException("Menu is not found or deleted");
            }

            return menuExist.MapToMenuDTO(_mapper);
        }
    }
}
