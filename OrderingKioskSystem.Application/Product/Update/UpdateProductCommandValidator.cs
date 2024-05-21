using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Product.Update
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(command => command.ID)
                .NotEmpty().WithMessage("ID can't be empty or null");

            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Name can't be empty or null")
                .MaximumLength(100).WithMessage("Name can't be over 100 words");

            RuleFor(command => command.Code)
                .NotEmpty().WithMessage("Code can't be empty or null");

            RuleFor(command => command.Url)
                .NotEmpty().WithMessage("Url can't be empty or null");

            RuleFor(command => command.Description)
                .NotEmpty().WithMessage("Description can't be empty or null")
                .MaximumLength(150).WithMessage("Description can't be over 150 words");

            RuleFor(command => command.Price)
                .NotEmpty().WithMessage("Price can't be empty or null")
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(command => command.CategoryID)
                .NotEmpty().WithMessage("CategoryID can't be empty or null");

            RuleFor(command => command.BusinessID)
                .NotEmpty().WithMessage("BussinessID can't be empty or null");
        }
    }
}
