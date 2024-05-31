using MediatR;
using OrderingKioskSystem.Domain.Repositories;
using OrderingKioskSystem.Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace OrderingKioskSystem.Application.Order.GetById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDTO>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public GetOrderByIdQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<OrderDTO> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var orderExist = await _orderRepository.FindAsync(x => x.ID == request.ID && !x.NgayXoa.HasValue, cancellationToken);

            if (orderExist is null)
            {
                throw new NotFoundException("Order does not exist");
            }

            return orderExist.MapToOrderDTO(_mapper);
        }
    }
}
