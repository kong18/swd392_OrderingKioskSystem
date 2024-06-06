using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.DashboardCategory.GetFoodMenu
{
    public class GetFoodMenuQuery : IRequest<List<FoodMenuDTO>>, IQuery
    {

    }
}
