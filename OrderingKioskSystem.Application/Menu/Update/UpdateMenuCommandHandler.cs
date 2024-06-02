using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Menu.Update
{
    public class UpdateMenuCommandHandler : IRequestHandler<UpdateMenuCommand, string>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IBusinessRepository _businessRepository;
        private readonly ICurrentUserService _currentUserService;
        public UpdateMenuCommandHandler(IMenuRepository menuRepository, ICurrentUserService currentUserService, IBusinessRepository businessRepository)
        {
            _menuRepository = menuRepository;
            _currentUserService = currentUserService;
            _businessRepository = businessRepository;
        }

        public async Task<string> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        {
            var menuExist = await _menuRepository.FindAsync(x => x.ID == request.ID && !x.NgayXoa.HasValue, cancellationToken);

            if (menuExist == null)
            {
                return "Menu is not found or deleted";
            }

            bool businessExist = await _businessRepository.AnyAsync(x => x.ID == request.BusinessID && !x.NgayXoa.HasValue, cancellationToken);

            if (!businessExist)
            {
                return "Business does not exist";
            }

            menuExist.Name = request.Name;
            menuExist.Type = request.Type;
            menuExist.Status = request.Status;
            menuExist.Title = request.Title;
            menuExist.BusinessID = request.BusinessID ?? menuExist.BusinessID;

            menuExist.NgayCapNhatCuoi = DateTime.Now;
            menuExist.NguoiCapNhatID = _currentUserService.UserId;

            _menuRepository.Update(menuExist);

            return await _menuRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Update Success!" : "Update Faile!";
        }
    }
}
