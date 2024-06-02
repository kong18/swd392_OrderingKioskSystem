using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using System;

namespace OrderingKioskSystem.Application.Order.Create
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDTO>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IKioskRepository _kioskRepository;
        private readonly OrderService _orderService;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        public CreateOrderCommandHandler(OrderService orderService,IOrderRepository orderRepository, IProductRepository productRepository, IOrderDetailRepository orderDetailRepository, IKioskRepository kioskRepository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _orderDetailRepository = orderDetailRepository;
            _kioskRepository = kioskRepository;
            _orderService = orderService;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<OrderDTO> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            bool kioskExist = await _kioskRepository.AnyAsync(x => x.ID == request.KioskID && !x.NgayXoa.HasValue, cancellationToken);

            if (!kioskExist)
            {
                throw new NotFoundException("Kiosk does not exist");
            }

            foreach (var item in request.Products)
            {
                bool productExist = await _productRepository.AnyAsync(x => x.ID == item.ProductID && !x.NgayXoa.HasValue, cancellationToken);

                if (!productExist)
                {
                    throw new NotFoundException("Product is not found or deleted");
                }
            }
            decimal total = 0;

            var order = new OrderEntity
            {
                KioskID = request.KioskID,
                Status = "OnPreparing",
                Note = request.Note ?? "",
                Total = total,
                //NguoiTaoID = _currentUserService.UserId,
                NgayTao = DateTime.Now
            };

            _orderRepository.Add(order);
            await _orderRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            var orderID = order.ID;

            foreach (var item in request.Products)
            {
                var product = await _productRepository.FindAsync(x => x.ID == item.ProductID, cancellationToken);
                var bonusPrice = 0;

                if (!string.IsNullOrEmpty(item.Size))
                {
                    
                    switch (item.Size)
                    {
                        case "S":
                            bonusPrice = 0;
                            break;
                        case "M":
                            bonusPrice = 5;
                            break;
                        case "L":
                            bonusPrice = 10;
                            break;
                        default:
                            bonusPrice = 0;
                            break;
                    }
                }

                var orderDetail = new OrderDetailEntity
                {
                    OrderID = orderID,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price + bonusPrice,
                    Price = item.Quantity * (product.Price + bonusPrice),
                    Size = item.Size?.ToUpper(),
                    OrderDate = DateTime.Now,
                    Status = true
                };

                _orderDetailRepository.Add(orderDetail);
                await _orderDetailRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

                total += orderDetail.Price;
            }

            order.Total = total;
            _orderRepository.Update(order);
            await _orderRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            await _orderService.NotifyNewOrder(orderID.ToString());

            return order.MapToOrderDTO(_mapper);
        }
    }
}
