using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Application.Menu.Update;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Menu.Delete
{
    public class DeleteMenuCommandHandler : IRequestHandler<DeleteMenuCommand, string>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly ICurrentUserService _currentUserService;
        public DeleteMenuCommandHandler(IMenuRepository menuRepository, ICurrentUserService currentUserService)
        {
            _menuRepository = menuRepository;
            _currentUserService = currentUserService;
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

            return await _menuRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Update Success!" : "Update Faile!";
        }
    }
}
