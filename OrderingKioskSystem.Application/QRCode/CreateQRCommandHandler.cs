using MediatR;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using SWD.OrderingKioskSystem.Application.Payment;
using SWD.OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.QRCode
{
    public class CreateQRCommandHandler : IRequestHandler<CreateQRCommand, string>
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IVietQrService _vietQrService;

        public CreateQRCommandHandler(IBusinessRepository businessRepository, IVietQrService vietQrService)
        {
            _businessRepository = businessRepository;
            _vietQrService = vietQrService;
        }

        public async Task<string> Handle(CreateQRCommand request, CancellationToken cancellationToken)
        {
            var business = await _businessRepository.FindAsync(x => x.ID == request.BusinessID, cancellationToken);

            if (business == null)
            {
                throw new NotFoundException("Business does not exist");
            }

            var qrCodeUrl = await _vietQrService.GeneratePaymentQrCode(
                business.BankAccountNumber,
                business.BankAccountName,
                business.BinId,
                request.Amount,
                "note"
            );

            return qrCodeUrl;
        }
    }
}
