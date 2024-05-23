using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OrderingKioskSystem.Application.Kiosk.Update
{
    public class UpdateKioskCommand : IRequest<string>, ICommand
    {
        public string Id { get; set; } // Use this as the primary key
        public string location { get; set; }  
        
    }
}
