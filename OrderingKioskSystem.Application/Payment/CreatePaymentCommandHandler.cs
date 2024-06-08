using MediatR;
using OrderingKioskSystem.Domain.Common.Exceptions;
using OrderingKioskSystem.Domain.Entities;
using OrderingKioskSystem.Domain.Repositories;
using SWD.OrderingKioskSystem.Application.Payment;
using SWD.OrderingKioskSystem.Domain.Repositories;

public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, string>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IBusinessRepository _businessRepository;
    private readonly IVietQrService _vietQrService;

    public CreatePaymentCommandHandler(IPaymentRepository paymentRepository, IOrderRepository orderRepository, IBusinessRepository businessRepository, IVietQrService vietQrService)
    {
        _paymentRepository = paymentRepository;
        _orderRepository = orderRepository;
        _businessRepository = businessRepository;
        _vietQrService = vietQrService;
    }

    public async Task<string> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        // Tìm order
        var order = await _orderRepository.FindAsync(x => x.ID == request.OrderID, cancellationToken);

        if (order == null || order.NgayXoa.HasValue)
        {
            throw new NotFoundException("Order does not exist");
        }

        var business = await _businessRepository.FindAsync(x => x.ID == request.BusinessID, cancellationToken);

        if (business == null)
        {
            throw new NotFoundException("Business does not exist");
        }

        var qrCodeUrl = await _vietQrService.GeneratePaymentQrCode(
            business.BankAccountNumber,
            business.BankAccountName,
            request.AcqId,
            request.Amount,
            request.AddInfo
        );

       
        var payment = new PaymentEntity
        {
            Amount = request.Amount,
            PaymentDate = DateTime.Now,
            OrderID = request.OrderID,
            PaymentGatewayID = request.PaymentGatewayID,
        };

        _paymentRepository.Add(payment);
        await _paymentRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return qrCodeUrl;
    }
}
