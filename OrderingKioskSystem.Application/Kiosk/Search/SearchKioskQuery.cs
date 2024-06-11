using MediatR;
using OrderingKioskSystem.Application.Kiosk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.Kiosk.Get
{
    public class SearchKioskQuery : IRequest<List<KioskDTO>>
    {
        public string? Title { get; set; }
        public string? Location { get; set; }
    }
}
