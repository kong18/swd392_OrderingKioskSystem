using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.PaymentGateway.Delete
{
    public class DeletePaymentGatewayCommandValidator :AbstractValidator<DeletePaymentGatewayCommand>
    {
        public DeletePaymentGatewayCommandValidator()
        {
            RuleFor(command => command.ID)
                .NotEmpty().WithMessage("ID can't be empty or null")
                .GreaterThan(0).WithMessage("ID must be a number");
        }
    }
}
