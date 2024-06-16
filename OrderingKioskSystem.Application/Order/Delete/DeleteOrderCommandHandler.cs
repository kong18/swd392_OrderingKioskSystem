using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Application.Order.Update;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Order.Delete
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, string>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly ICurrentUserService _currentUserService;
        public DeleteOrderCommandHandler(IOrderRepository orderRepository, ICurrentUserService currentUserService, IOrderDetailRepository orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _currentUserService = currentUserService;
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<string> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderExist = await _orderRepository.FindAsync(x => x.ID == request.ID && !x.NgayXoa.HasValue, cancellationToken);

            if (orderExist is null)
            {
                return "Order does not exist";
            }

            var orderDetail = await _orderDetailRepository.FindAllAsync(x => x.OrderID ==  request.ID, cancellationToken);

            foreach( var order in orderDetail)
            {
                order.Status = false;
                _orderDetailRepository.Update(order);
                await _orderDetailRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }

            orderExist.NgayXoa = DateTime.UtcNow.AddHours(7);
            orderExist.NguoiXoaID = _currentUserService.UserId;

            _orderRepository.Update(orderExist);
            return await _orderRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Delete Success!" : "Delete Fail!";
        }
    }
}
