using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Business.GetAllBusiness
{
    public class GetBusinessByFilterQuery : IRequest<List<BusinessDTO>>, IQuery
    {
        public string Name { get; set; }
        public string BankName { get; set; }
    }
}
