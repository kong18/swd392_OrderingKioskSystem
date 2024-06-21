using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.Kiosk.Login
{
    public class KioskLoginCommandHandler : IRequestHandler<KioskLoginCommand, string>
    {
        private readonly IKioskRepository _kioskRepository;
        private readonly IJwtService _jwtService;

        public KioskLoginCommandHandler(IKioskRepository kioskRepository, IJwtService jwtService)
        {
            _kioskRepository = kioskRepository;
            _jwtService = jwtService;
        }

        public async Task<string> Handle(KioskLoginCommand request, CancellationToken cancellationToken)
        {
            var kiosk = await _kioskRepository.FindAsync(k => k.Code == request.Code && k.PIN == request.PIN);

            if (kiosk == null)
            {
                throw new NotFoundException("Invalid kiosk code or PIN.");
            }

            var token = _jwtService.CreateToken(kiosk.Code, "Kiosk", 30); // 30 days token

            return token;
        }
    }
}
