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
            RuleFor(command => command.id)
                .NotEmpty().WithMessage("ID can't be empty or null");

            RuleFor(command => command.name)
                .MaximumLength(100).WithMessage("Name can't be over 100 words");

            RuleFor(command => command.description)
                .MaximumLength(150).WithMessage("Description can't be over 150 words");

            RuleFor(command => command.price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");

        }
    }
}
