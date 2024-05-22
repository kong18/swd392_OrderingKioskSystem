using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Kiosk.Delete
{
    public class DeleteKioskCommand : IRequest<string>,ICommand
    {
        public string Id {  get; set; }
        public DeleteKioskCommand(string id)
        {
            Id = id;
        }
    }
}
