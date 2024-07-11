using MediatR;
using Microsoft.Extensions.Caching.Memory;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Application.Menu.Update;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Menu.Delete
{
    public class DeleteMenuCommandHandler : IRequestHandler<DeleteMenuCommand, string>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMemoryCache _cache;

        public DeleteMenuCommandHandler(IMenuRepository menuRepository, ICurrentUserService currentUserService, IMemoryCache cache)
        {
            _menuRepository = menuRepository;
            _currentUserService = currentUserService;
            _cache = cache;
        }

        public async Task<string> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
        {
            var menuExist = await _menuRepository.FindAsync(x => x.ID == request.ID && !x.NgayXoa.HasValue, cancellationToken);

            if (menuExist == null)
            {
                return "Menu is not found or deleted";
            }

            menuExist.NgayXoa = DateTime.Now;
            menuExist.NguoiTaoID = _currentUserService.UserId;

            _menuRepository.Update(menuExist);

            if (await _menuRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0)
            {
                // Remove the menu from the cache
                _cache.Remove(menuExist.ID);
                return "Delete Success!";
            }
            else
            {
                return "Delete Fail!";
            }
        }
    }
}
