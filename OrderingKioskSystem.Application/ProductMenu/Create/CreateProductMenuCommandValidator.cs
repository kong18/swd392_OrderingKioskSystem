using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.ProductMenu.Create
{
    public class CreateProductMenuCommandValidator : AbstractValidator<CreateProductMenuCommand>
    {
        public CreateProductMenuCommandValidator()
        {
            RuleFor(command => command.MenuID)
                .NotEmpty().WithMessage("MenuID can't be empty or null");

            RuleFor(command => command.Products)
                .NotEmpty().WithMessage("Product's list can't be empty or null")
                .Must(items => items != null && items.Count > 0).WithMessage("Items list must contain at least one item");

            RuleForEach(command => command.Products)
            .SetValidator(new RequestItemValidator());
        }

        public class RequestItemValidator : AbstractValidator<RequestItem>
        {
            public RequestItemValidator()
            {
                RuleFor(item => item.ProductID)
                    .NotEmpty().WithMessage("ProductID can't be empty or null");

                RuleFor(item => item.Price)
                    .NotEmpty().WithMessage("Price can't be empty or null")
                    .GreaterThan(0).WithMessage("Price must be greater than 0");
            }
        }
    }
}
