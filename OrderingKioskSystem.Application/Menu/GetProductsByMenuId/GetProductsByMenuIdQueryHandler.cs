using AutoMapper;
using MediatR;
using OrderingKioskSystem.Application.Product;
using OrderingKioskSystem.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.Menu.GetProductsByMenuId
{
    public class GetProductsByMenuIdQueryHandler : IRequestHandler<GetProductsByMenuIdQuery, List<ProductDTO>>
    {
        private readonly IProductMenuRepository _productMenuRepository;
        private readonly IMapper _mapper;

        public GetProductsByMenuIdQueryHandler(IProductMenuRepository productMenuRepository, IMapper mapper)
        {
            _productMenuRepository = productMenuRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDTO>> Handle(GetProductsByMenuIdQuery request, CancellationToken cancellationToken)
        {
            var productMenus = await _productMenuRepository
                .FindAllAsync(pm => pm.MenuID == request.MenuId, cancellationToken);

            var products = productMenus.Select(pm => pm.Product).ToList();

            return _mapper.Map<List<ProductDTO>>(products);
        }
    }
}
