using MediatR;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.PaymentGateway.Update
{
    public class UpdatePaymentGatewayCommandHandler : IRequestHandler<UpdatePaymentGatewayCommand, string>
    {
        private readonly IPaymentGatewayRepository _paymentGatewayRepository;

        public UpdatePaymentGatewayCommandHandler(IPaymentGatewayRepository paymentGatewayRepository)
        {
            _paymentGatewayRepository = paymentGatewayRepository;
        }

        public async Task<string> Handle(UpdatePaymentGatewayCommand request, CancellationToken cancellationToken)
        {
            var paymentGateways = await _paymentGatewayRepository.FindAllAsync(x => (x.ID == request.ID || x.Name.ToLower().Equals(request.Name.ToLower())) && !x.NgayXoa.HasValue, cancellationToken);

            if (paymentGateways.Count == 0)
            {
                throw new NotFoundException("PaymentGateway not found");
            }

            if (paymentGateways.Count >= 2)
            {
                throw new DuplicationException("Name can't be dupplicated!");
            }

            if (paymentGateways[0].Name.ToLower().Equals(request.Name.ToLower()))
            {
                throw new DuplicationException("Name can't be dupplicated!");
            }

            paymentGateways[0].Name = request.Name;
            _paymentGatewayRepository.Update(paymentGateways[0]);
            return await _paymentGatewayRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Update Success!" : "Update Failed!";

        }
    }
}
