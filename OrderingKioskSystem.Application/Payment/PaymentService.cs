using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using SWD.OrderingKioskSystem.Application.VNPay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentGatewayRepository _paymentGatewayRepository;

        public PaymentService(IPaymentRepository paymentRepository, IPaymentGatewayRepository paymentGatewayRepository)
        {
            _paymentRepository = paymentRepository;
            _paymentGatewayRepository = paymentGatewayRepository;
        }
        public async Task SavePaymentAsync(TransactionResponsePaymentDTO response)
        {
            if (response == null || !response.Success || response.VnPayResponseCode != "00")
            {
                throw new ArgumentException("Invalid payment response");
            }

            // Check if the payment gateway exists
            var paymentGateway = await _paymentGatewayRepository.FindAsync(x => x.Name.ToLower().Equals(response.PaymentMethod.ToLower()));

            if (paymentGateway == null)
            {
                // Add a new payment gateway
                paymentGateway = new PaymentGatewayEntity
                {
                    Name = response.PaymentMethod.ToLower()
                };

                _paymentGatewayRepository.Add(paymentGateway);
                await _paymentGatewayRepository.UnitOfWork.SaveChangesAsync();
            }

            var payment = new PaymentEntity
            {
                ID = response.PaymentId,
                TransactionId = response.TransactionId,
                OrderID = response.OrderId,
                Amount = response.Amount/100,
                PaymentGatewayID = paymentGateway.ID,
                Token = response.Token,
                PaymentDate = DateTime.UtcNow
            };

            _paymentRepository.Add(payment);
            await _paymentRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
