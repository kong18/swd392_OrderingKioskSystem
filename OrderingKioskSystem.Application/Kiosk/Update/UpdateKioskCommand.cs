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
        public UpdateKioskCommand(string id, string? location, string? code, int? pin)
        {
            Id = id;
            Location = location;
            Code = code;
            PIN = pin;
        }

        public string Id { get; set; } // Use this as the primary key
        public string? Location { get; set; }  
        public string? Code { get; set; }
        public int? PIN { get; set; }
        
    }
}
