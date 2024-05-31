using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Application.Order.Create;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using OrderingKioskSystem.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Order.Update
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, string>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShipperRepository _shipperRepository;
        private readonly ICurrentUserService _currentUserService;
        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IShipperRepository shipperRepository, ICurrentUserService currentUserService)
        {
            _orderRepository = orderRepository;
            _shipperRepository = shipperRepository;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderExist = await _orderRepository.FindAsync(x => x.ID == request.ID && !x.NgayTao.HasValue, cancellationToken);

            if (orderExist is null)
            {
                return "Order does not exist";
            }

            if (request.ShipperID != null)
            {
                bool shipperExist = await _shipperRepository.AnyAsync(x => x.ID == request.ShipperID && !x.NgayXoa.HasValue, cancellationToken);

                if (!shipperExist)
                {
                    return "Shipper does not exist";
                }
            }

            orderExist.Status = request.Status ?? orderExist.Status;
            orderExist.ShipperID = request.ShipperID ?? orderExist.ShipperID;

            orderExist.NguoiCapNhatID = _currentUserService.UserId;
            orderExist.NgayCapNhatCuoi = DateTime.Now;
            _orderRepository.Update(orderExist);

            return await _orderRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Update Success!" : "Update Fail!";
        }
    }
}
