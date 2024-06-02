using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Order.Update
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(command => command.ID)
                .NotEmpty().WithMessage("OrderID can't be empty or null");

            RuleFor(command => command.Status)
                .NotEmpty().WithMessage("Status can't be empty or null")
                .Must(status => new[] { "OnPreparing", "Prepared", "OnDelivering", "Delivered" }
                .Contains(status))
                .WithMessage("Status must be one of the following values: OnPreparing, Prepared, OnDelivering, Delivered");
        }
    }
}
