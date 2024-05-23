using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Business.Delete
{
    public class DeleteBusinessCommand : IRequest<string>
    {
        public string Id;
        public DeleteBusinessCommand(string id ) {
            Id = id;
        }
    }
}
