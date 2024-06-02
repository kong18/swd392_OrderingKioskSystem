using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Menu.GetById
{
    public class GetMenuByIdQuery : IRequest<MenuDTO>
    {
        public GetMenuByIdQuery(string iD)
        {
            ID = iD;
        }

        public string ID { get; set; }
    }
}
