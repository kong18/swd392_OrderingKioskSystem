using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.ProductMenu.Delete
{
    public class DeleteProductMenuCommandValidator : AbstractValidator<DeleteProductMenuCommand>
    {
        public DeleteProductMenuCommandValidator()
        {
            RuleFor(command => command.MenuID)
                .NotEmpty().WithMessage("MenuID can't be null or empty");

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
            }
        }
    }
}
