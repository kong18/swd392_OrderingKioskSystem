using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Kiosk.GetById
{
    public class GetKioskByIdQuery : IRequest<KioskDTO>
    {
        public string Id { get; set; }  

        public GetKioskByIdQuery(string id)
        {
            Id = id;
        }   
    }
}
