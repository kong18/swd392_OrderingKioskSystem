using FluentValidation;
using OrderingKioskSystem.Application.Order.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Menu.Create
{
    public class CreateMenuCommandValidator : AbstractValidator<CreateMenuCommand>
    {
        private readonly string[] validTypes = { "Morning", "Afternoon", "Evening" };
        public CreateMenuCommandValidator() 
        {
                RuleFor(command => command.Name)
                    .NotEmpty().WithMessage("Name can't be empty or null");

                RuleFor(command => command.Title)
                    .NotEmpty().WithMessage("Title can't be empty or null");

                RuleFor(command => command.Type)
                    .Must(type => Array.Exists(validTypes, s => s.Equals(type, StringComparison.OrdinalIgnoreCase)))
                    .WithMessage("Type must be one of the predefined values: Morning, Afternoon, Evening");

                RuleFor(command => command.BusinessID)
                    .NotEmpty().WithMessage("BusinessID can't be empty or null");

                RuleFor(command => command.Status)
                    .NotEmpty().WithMessage("Status can't be empty or null");

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
