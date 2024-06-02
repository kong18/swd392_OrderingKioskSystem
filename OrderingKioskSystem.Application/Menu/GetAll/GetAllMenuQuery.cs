using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Menu.GetAll
{
    public class GetAllMenuQuery : IRequest<List<MenuDTO>>
    {
        public GetAllMenuQuery() { }
    }
}
