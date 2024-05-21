using AutoMapper;
using InternSystem.Infrastructure.Repositories;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using OrderingKioskSystem.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Infrastructure.Repositories
{
    public class ShipperRepository : RepositoryBase<ShipperEntity, ShipperEntity, ApplicationDbContext>, IShipperRepository
    {
        public ShipperRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }
    }
}
