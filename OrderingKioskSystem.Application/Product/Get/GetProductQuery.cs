﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Product.GetAll
{
    public class GetProductQuery : IRequest<List<ProductDTO>>
    {
        public GetProductQuery() { }
    }
}