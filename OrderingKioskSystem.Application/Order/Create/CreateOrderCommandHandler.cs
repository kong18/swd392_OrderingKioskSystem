using MediatR;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;

namespace OrderingKioskSystem.Application.Order.Create
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IKioskRepository _kioskRepository;
        private readonly OrderService _orderService;
        public CreateOrderCommandHandler(OrderService orderService,IOrderRepository orderRepository, IProductRepository productRepository, IOrderDetailRepository orderDetailRepository, IKioskRepository kioskRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _orderDetailRepository = orderDetailRepository;
            _kioskRepository = kioskRepository;
            _orderService = orderService;
        }

        public async Task<CreateOrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            bool kioskExist = await _kioskRepository.AnyAsync(x => x.ID == request.KioskID && !x.NgayXoa.HasValue, cancellationToken);

            if (!kioskExist)
            {
                throw new NotFoundException("Kiosk does not exist");
            }

            foreach (var item in request.Items)
            {
                bool productExist = await _productRepository.AnyAsync(x => x.ID == item.ProductID && !x.NgayXoa.HasValue, cancellationToken);

                if (!productExist)
                {
                    throw new NotFoundException("Product is not found or deleted");
                }
            }

                    var response = new CreateOrderResponse();
            var listResponseItem = new List<ResponseItem>();
            decimal total = 0;

            var order = new OrderEntity
            {
                KioskID = request.KioskID,
                Status = "OnPreparing",
                Note = request.Note ?? "",
                Total = total,
            };

            _orderRepository.Add(order);
            await _orderRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            var orderID = order.ID;

            foreach (var item in request.Items)
            {
                var product = await _productRepository.FindAsync(x => x.ID == item.ProductID, cancellationToken);

                var responseItem = new ResponseItem
                {
                    ProductID = product.ID,
                    Name = product.Name,
                    UnitPrice = product.Price,
                    Quantity = item.Quantity,
                    Price = product.Price * item.Quantity,
                    Size = item.Size,
                };

                var orderDetail = new OrderDetailEntity
                {
                    OrderID = orderID,
                    ProductID = product.ID,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price,
                    Price = item.Quantity * product.Price,
                    Size = item.Size,
                    OrderDate = DateTime.Now,
                    Status = true
                };

                _orderDetailRepository.Add(orderDetail);
                await _orderDetailRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

                total += responseItem.Price;
                listResponseItem.Add(responseItem);
            }

            order.Total = total;
            _orderRepository.Update(order);
            await _orderRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            response.Products = listResponseItem;
            response.Total = total;
            response.KioskID = request.KioskID;
            response.ID = orderID.ToString();

            await _orderService.NotifyNewOrder(orderID.ToString());

            return response;
        }
    }
}
