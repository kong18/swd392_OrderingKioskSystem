using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.PaymentGateway.Update
{
    public class UpdatePaymentGatewayCommandValidator : AbstractValidator<UpdatePaymentGatewayCommand>
    {
        public UpdatePaymentGatewayCommandValidator()
        {
            RuleFor(command => command.ID)
                .NotEmpty().WithMessage("ID can't be empty or null")
                .GreaterThan(0).WithMessage("ID must be a number");

            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Name can't be empty or null")
                .MaximumLength(100).WithMessage("Name can't over 100 words")
                .Matches(@"^[a-zA-Z\s]*$").WithMessage("Name can't contain special characters");
        }
    }
}
