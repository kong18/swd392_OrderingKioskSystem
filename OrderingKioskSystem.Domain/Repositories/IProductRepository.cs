using OrderingKioskSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Domain.Repositories
{
    public interface IProductRepository : IEFRepository<ProductEntity, ProductEntity>
    {

    }
}
