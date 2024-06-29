﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Application.Kiosk.GetById;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

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

        public CreateOrderCommandHandler(OrderService orderService, IOrderRepository orderRepository, IProductRepository productRepository, IOrderDetailRepository orderDetailRepository, IKioskRepository kioskRepository, IMapper mapper, ICurrentUserService currentUserService)
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
            var kioskCode = _currentUserService.UserId;
            // Check if the kiosk exists
            var kioskExist = await _kioskRepository.FindAsync(x => x.Code == kioskCode && !x.NgayXoa.HasValue, cancellationToken);

            if (kioskExist is null)
            {
                throw new NotFoundException("Kiosk does not exist");
            }

            // Check if all products exist
            foreach (var item in request.Products)
            {
                bool productExist = await _productRepository.AnyAsync(x => x.ID == item.ProductID && !x.NgayXoa.HasValue, cancellationToken);

                if (!productExist)
                {
                    throw new NotFoundException($"Product with ID {item.ProductID} is not found or deleted");
                }
            }

            var order = new OrderEntity
            {
                KioskID = kioskExist.ID,
                Status = "Pending",
                Note = request.Note ?? "",
                Total = request.Total,
                NguoiTaoID = _currentUserService.UserId,
                NgayTao = DateTime.UtcNow.AddHours(7)
            };

            _orderRepository.Add(order);
            await _orderRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            var orderID = order.ID;

            foreach (var item in request.Products)
            {
                var product = await _productRepository.FindAsync(x => x.ID == item.ProductID, cancellationToken);
                var bonusPrice = item.Size?.ToUpper() switch
                {
                    "M" => 5000,
                    "L" => 10000,
                    _ => 0
                };

                var orderDetail = new OrderDetailEntity
                {
                    OrderID = orderID,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price + bonusPrice,
                    Price = item.Quantity * (product.Price + bonusPrice),
                    Size = item.Size?.ToUpper(),
                    OrderDate = DateTime.UtcNow.AddHours(7),
                    Status = true
                };

                _orderDetailRepository.Add(orderDetail);
                await _orderDetailRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }
            await _orderService.NotifyNewOrder(orderID.ToString());

            return order.MapToOrderDTO(_mapper);
        }
    }
}
