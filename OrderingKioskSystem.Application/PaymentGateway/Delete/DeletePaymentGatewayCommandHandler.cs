using MediatR;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.PaymentGateway.Delete
{
    public class DeletePaymentGatewayCommandHandler : IRequestHandler<DeletePaymentGatewayCommand, string>
    {
        private readonly IPaymentGatewayRepository _paymentGatewayRepository;

        public DeletePaymentGatewayCommandHandler(IPaymentGatewayRepository paymentGatewayRepository)
        {
            _paymentGatewayRepository = paymentGatewayRepository;
        }

        public async Task<string> Handle(DeletePaymentGatewayCommand request, CancellationToken cancellationToken)
        {
            var paymentGateway = await _paymentGatewayRepository.FindAsync(x => x.ID == request.ID && !x.NgayXoa.HasValue, cancellationToken);

            if (paymentGateway == null)
            {
                throw new NotFoundException("PaymentGateway not found");
            }

            paymentGateway.NgayXoa = DateTime.UtcNow.AddHours(7);
            _paymentGatewayRepository.Update(paymentGateway);
            return await _paymentGatewayRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Delete Success!" : "Delete Failed!";

        }
    }
}
