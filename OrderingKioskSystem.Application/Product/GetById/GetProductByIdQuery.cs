using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Product.GetById
{
    public class GetProductByIdQuery : IRequest<ProductDTO>, IQuery
    {
        public GetProductByIdQuery(string id)
        {
            ID = id;
        }
        public string ID { get; set; }
    }
}
