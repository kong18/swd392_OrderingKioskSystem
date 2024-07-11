using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Menu.GetById
{
    public class GetMenuByIdQueryHandler : IRequestHandler<GetMenuByIdQuery, MenuDTO>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private const string CachePrefix = "Menu_";

        public GetMenuByIdQueryHandler(IMenuRepository menuRepository, IMapper mapper, IMemoryCache cache)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<MenuDTO> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
        {
            string cacheKey = $"{CachePrefix}{request.ID}";

            if (!_cache.TryGetValue(cacheKey, out MenuDTO menuDto))
            {
                var menuExist = await _menuRepository.FindAsync(x => x.ID == request.ID && !x.NgayXoa.HasValue, cancellationToken);

                if (menuExist == null)
                {
                    throw new NotFoundException("Menu is not found or deleted");
                }

                menuDto = menuExist.MapToMenuDTO(_mapper);

                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };
                _cache.Set(cacheKey, menuDto, cacheOptions);
            }

            return menuDto;
        }
    }
}
