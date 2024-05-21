using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Order.Create
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator() 
        {
            RuleFor(command => command.KioskID)
                .NotEmpty().WithMessage("KioskID can't be empty or null");

            RuleFor(command => command.KioskID)
                .NotEmpty().WithMessage("KioskID can't be empty or null");

            RuleFor(command => command.Items)
                .NotEmpty().WithMessage("Items list can't be empty or null")
                .Must(items => items != null && items.Count > 0).WithMessage("Items list must contain at least one item");

            RuleForEach(command => command.Items)
            .SetValidator(new RequestItemValidator());
        }
    }

    public class RequestItemValidator : AbstractValidator<RequestItem>
    {
        private readonly string[] validSizes = { "small", "medium", "large" };
        public RequestItemValidator()
        {

            RuleFor(item => item.ProductID)
                .NotEmpty().WithMessage("ProductID can't be empty or null");

            RuleFor(item => item.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0");

            RuleFor(item => item.Size)
            .Must(size => string.IsNullOrEmpty(size) || Array.Exists(validSizes, s => s.Equals(size, StringComparison.OrdinalIgnoreCase)))
            .WithMessage("Size must be either empty or one of the predefined values: small, medium, large");
        }
    }
}
