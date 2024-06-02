using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Menu.Delete
{
    public class DeleteMenuCommand : IRequest<string>
    {
        public DeleteMenuCommand(string id)
        {
            ID = id;
        }

        public string ID { get; set; }
    }
}
