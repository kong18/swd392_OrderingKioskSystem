using MediatR;
using OrderingKioskSystem.Domain.Repositories;
using OrderingKioskSystem.Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingKioskSystem.Domain.Entities;

namespace SWD.OrderingKioskSystem.Application.PaymentGateway.Create
{
    public class CreatePaymentGatewayCommandHandler : IRequestHandler<CreatePaymentGatewayCommand, string>
    {
        private readonly IPaymentGatewayRepository _paymentGatewayRepository;

        public CreatePaymentGatewayCommandHandler(IPaymentGatewayRepository paymentGatewayRepository)
        {
            _paymentGatewayRepository = paymentGatewayRepository;
        }

        public async Task<string> Handle(CreatePaymentGatewayCommand request, CancellationToken cancellationToken)
        {
            var paymentGateway = await _paymentGatewayRepository.FindAsync(x => x.Name.ToLower().Equals(request.Name.ToLower()) && !x.NgayXoa.HasValue, cancellationToken);

            if (paymentGateway != null)
            {
                if (paymentGateway.NgayXoa != null)
                {
                    paymentGateway.NgayXoa = null;
                    _paymentGatewayRepository.Update(paymentGateway);

                    return await _paymentGatewayRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Create Success!" : "Create Failed!";
                }

                throw new DuplicationException("Name can't be dupplicated!");
            }

            var p = new PaymentGatewayEntity
            {
                Name = request.Name,
            };

            _paymentGatewayRepository.Add(p);
            return await _paymentGatewayRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Create Success!" : "Create Failed!";
        }
    }
}
