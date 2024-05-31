using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Order.GetById;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Order.GetAll
{
    public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, List<OrderDTO>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public GetAllOrderQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<List<OrderDTO>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            var list = await _orderRepository.FindAllAsync(x => !x.NgayXoa.HasValue, cancellationToken);

            if (list is null)
            {
                throw new NotFoundException("Order's list is empty");
            }

            return list.MapToOrderDTOList(_mapper);
        }
    }
}
