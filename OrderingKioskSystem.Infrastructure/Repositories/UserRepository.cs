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
    public class UserRepository : RepositoryBase<UserEntity, UserEntity, ApplicationDbContext>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }
    }
}
