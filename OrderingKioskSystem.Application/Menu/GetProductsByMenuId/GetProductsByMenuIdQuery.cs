using MediatR;
using OrderingKioskSystem.Application.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.Menu.GetProductsByMenuId
{
    public class GetProductsByMenuIdQuery : IRequest<List<ProductDTO>>
    {
        public string MenuId { get; set; }

        public GetProductsByMenuIdQuery(string menuId)
        {
            MenuId = menuId;
        }
    }
}
