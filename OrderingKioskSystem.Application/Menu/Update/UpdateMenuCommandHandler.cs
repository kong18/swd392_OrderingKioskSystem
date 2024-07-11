using MediatR;
using Microsoft.Extensions.Caching.Memory;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Menu.Update
{
    public class UpdateMenuCommandHandler : IRequestHandler<UpdateMenuCommand, string>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IBusinessRepository _businessRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMemoryCache _cache;

        public UpdateMenuCommandHandler(
            IMenuRepository menuRepository,
            ICurrentUserService currentUserService,
            IBusinessRepository businessRepository,
            IMemoryCache cache)
        {
            _menuRepository = menuRepository;
            _currentUserService = currentUserService;
            _businessRepository = businessRepository;
            _cache = cache;
        }

        public async Task<string> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        {
            var menuExist = await _menuRepository.FindAsync(x => x.ID == request.ID && !x.NgayXoa.HasValue, cancellationToken);

            if (menuExist == null)
            {
                throw new NotFoundException("Menu is not found or deleted");
            }

            bool menuExistwithType = await _menuRepository.AnyAsync(x => x.BusinessID == _currentUserService.UserId && x.Type == request.Type && !x.NgayXoa.HasValue, cancellationToken);

            if (menuExistwithType)
            {
                throw new DuplicationException("Business can't update menu with Type already exist!");
            }

            menuExist.Name = request.Name ?? menuExist.Name;
            menuExist.Type = request.Type ?? menuExist.Type;
            menuExist.Status = request.Status ?? menuExist.Status;
            menuExist.Title = request.Title ?? menuExist.Title;

            menuExist.NgayCapNhatCuoi = DateTime.Now;
            menuExist.NguoiCapNhatID = _currentUserService.UserId;

            _menuRepository.Update(menuExist);

            if (await _menuRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0)
            {
                // Update the cache
                _cache.Set(menuExist.ID, menuExist, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                });

                return "Update Success!";
            }
            else
            {
                return "Update Fail!";
            }
        }
    }
}
