using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.User
{
    public class DeleteManagerCommand : IRequest<string>
    {
        public string Id;
        public DeleteManagerCommand(string id)
        {
            Id = id;
        }
    }
}
