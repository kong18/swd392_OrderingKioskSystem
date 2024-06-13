using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Product.Create
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator() 
        {
            RuleFor(command => command.name)
                .NotEmpty().WithMessage("Name can't be empty or null")
                .MaximumLength(100).WithMessage("Name can't be over 100 words");

            RuleFor(command => command.code)
                .NotEmpty().WithMessage("Code can't be empty or null");

          
            RuleFor(command => command.description)
                .NotEmpty().WithMessage("Description can't be empty or null")
                .MaximumLength(150).WithMessage("Description can't be over 150 words");

            RuleFor(command => command.price)
                .NotEmpty().WithMessage("Price can't be empty or null")
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(command => command.categoryname)
                .NotEmpty().WithMessage("CategoryID can't be empty or null");
        }
    }
}
