using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingKioskSystem.Application.Common.Interfaces;
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
            bool kioskExist = await _kioskRepository.AnyAsync(x => x.ID == request.KioskID && !x.NgayXoa.HasValue, cancellationToken);

            if (!kioskExist)
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

            var dbContext = _orderRepository.UnitOfWork as DbContext;
            if (dbContext == null)
            {
                throw new InvalidOperationException("The UnitOfWork is not associated with a DbContext.");
            }

            // Check if there's an ambient transaction and throw an exception if there is
            if (System.Transactions.Transaction.Current != null)
            {
                throw new InvalidOperationException("An ambient transaction is already in progress.");
            }

            using (var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    decimal total = 0;

                    // Create and add the new order
                    var order = new OrderEntity
                    {
                        KioskID = request.KioskID,
                        Status = "OnPreparing",
                        Note = request.Note ?? "",
                        Total = total,
                        NguoiTaoID = _currentUserService.UserId,
                        NgayTao = DateTime.Now
                    };

                    _orderRepository.Add(order);
                    await _orderRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                    var orderID = order.ID;

                    // Add each product to the order
                    foreach (var item in request.Products)
                    {
                        var product = await _productRepository.FindAsync(x => x.ID == item.ProductID, cancellationToken);
                        var bonusPrice = 0;

                        if (!string.IsNullOrEmpty(item.Size))
                        {
                            switch (item.Size.ToUpper())
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

                        total += orderDetail.Price;
                    }

                    // Save changes for all order details in a single transaction
                    await _orderDetailRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

                    // Update the total price of the order
                    order.Total = total;
                    _orderRepository.Update(order);
                    await _orderRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

                    // Commit the transaction
                    await transaction.CommitAsync(cancellationToken);

                    // Notify about the new order
                    await _orderService.NotifyNewOrder(orderID.ToString());

                    // Map and return the order DTO
                    return order.MapToOrderDTO(_mapper);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    // Log the exception (ex)
                    throw new ApplicationException($"An error occurred while creating the order: {ex.Message}", ex);
                }
            }
        }
    }
}
